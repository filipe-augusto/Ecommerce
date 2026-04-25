using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class TokenService(IConfiguration config)
{
    public string GenerateToken(Usuario usuario)
    {
      
        //PEGAR CHAVE
        var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]!);
       // DEFINIR CLAIMS
       var claims = new List<Claim>()
       {
         new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
         new Claim(ClaimTypes.Name, usuario.Nome),
         new Claim(ClaimTypes.Email, usuario.Email),
         new Claim(ClaimTypes.Role, usuario.perfil)
       };
    //CONFIGURAR O TOKEN
       var tokenDescriptor = new SecurityTokenDescriptor
       {
        Subject = new ClaimsIdentity(claims),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(
            new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        Issuer = config["Jwt:Issuer"],
        Audience = config["Jwt:Audience"]
       
       };
        //GERAR STRING
       var tokenHandler = new JwtSecurityTokenHandler();
       var token =  tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}