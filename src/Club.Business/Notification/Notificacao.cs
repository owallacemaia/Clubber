using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Club.Business.Notification
{
    public partial class Notificacao
    {
        public string Mensagem { get; set; }

        public Notificacao(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
