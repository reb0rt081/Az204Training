namespace Az204.Model.EntitiesLayer.Entities
{
    //  Entidad susceptible de ser persistida: propiedad Guid Id necesaria para identificarla con la clave primaria de acceso
    public class Login : EntityBase
    {
        public string Name { get; set; }

        //  Nunca guardes una contraseña sin transformar en Hash para que no sea legible
        public string Password { get; set; }
    }
}
