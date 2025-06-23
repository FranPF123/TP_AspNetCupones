using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Jwt
{
	public static class HerramientaJwt
	{
		//Herramienta para obtener datos del usuario guardado en los claims
		public static string obtenerClaim(string token, string nombreDelClaim)
		{
			var handler = new JwtSecurityTokenHandler();
			var jwtToKn = handler.ReadJwtToken(token);

			var claim = jwtToKn.Claims.FirstOrDefault(c => c.Type == nombreDelClaim);
			if(claim == null) return null;

			return claim.Value;
			
		}
	}
}
