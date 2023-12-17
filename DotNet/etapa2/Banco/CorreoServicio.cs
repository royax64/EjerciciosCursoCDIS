namespace Banco;
using MimeKit;
using MailKit.Net.Smtp;

public static class CorreoServicio {

    public static void enviar(){
        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress ("Yo","yo@hotmail.com"));
        mensaje.To.Add(new MailboxAddress ("Yo malo","yomalo@hotmail.com"));
        mensaje.Subject = "Tus nuevos usuarios de hoy";
        mensaje.Body = new TextPart("plain") {Text = obtenerUsuariosHoy()};
        
        using (var cliente = new SmtpClient()){
            try{
                cliente.Connect("smtp-mail.outlook.com", 587, false);
                cliente.Authenticate("yo@hotmail.com", "ChupaUnPerro");
                cliente.Send(mensaje);
                cliente.Disconnect(true);
            } catch (MailKit.Security.AuthenticationException e){
                Console.WriteLine("Error: Usuario o contraseña incorrecta.");
            } catch (Exception e){
                Console.WriteLine("Error: No se pudo enviar tu correo. ¿Tienes internet?");
            }

        }
    }

    private static string obtenerUsuariosHoy(){
        List<Usuario> usrHoy = Almacenaje.getNuevosUsuarios();
        if (!usrHoy.Any()) return "No hay usuarios registrados hoy";

        string texto = "Usuarios agregados hoy:\n";

        foreach (Usuario usr in usrHoy){
            texto += $"\t{usr.verInfo()}\n";
        }

        return texto;
    }
}
