namespace Banco;
using MimeKit;
using MailKit.Net.Smtp;

public static class CorreoServicio {

    public static void enviar(){
        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress ("Yo","yo@hotmail.com"));
        mensaje.To.Add(new MailboxAddress ("Yo también","yoTambien@hotmail.com"));
        mensaje.Subject = "Tus nuevos usuarios de hoy";
        mensaje.Body = new TextPart("plain") {Text = obtenerUsuariosHoy()};
        
        using (var cliente = new SmtpClient()){
            cliente.Connect("smtp-mail.outlook.com", 587, false);
            cliente.Authenticate("yo@hotmail.com", "chupaUnPerro");
            cliente.Send(mensaje);
            cliente.Disconnect(true);
        }
    }

    private static string obtenerUsuariosHoy(){
        List<Usuario> usrHoy = Guardar.getNuevosUsuarios();
        if (usrHoy.Count == 0) return "No hay usuarios registrados hoy";

        string texto = "Usuarios agregados hoy:\n";

        foreach (Usuario usr in usrHoy){
            texto += $"\t{usr.verInfo()}\n";
        }

        return texto;
    }
}