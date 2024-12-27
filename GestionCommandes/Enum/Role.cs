namespace GestionCommandes.Enum
{
    public enum Role 
    {
        CLIENT,
        RS,
        COMPTABLE,
    }

    public class RoleHelper
    {
        public static Array GetRoles()
        {
            return Role.GetValues(typeof(Role));
        }
    }
}