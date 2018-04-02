using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace chat_cliente
{
    

    public partial class Form1 : Form
    {

        UdpClient udpclient;
        IPEndPoint remoteep;

        

        public Form1()
        {
            InitializeComponent();

            udpclient = new UdpClient(2);                                   //Se puede poner cualquier otro puerto !=2
            IPAddress multicastaddress = IPAddress.Parse("224.0.0.1");
            udpclient.JoinMulticastGroup(multicastaddress);
            remoteep = new IPEndPoint(multicastaddress, 8080);
        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] sendBytes = Encoding.Unicode.GetBytes(richTextBox1.Text);
            Send(sendBytes, sendBytes.Length, remoteep);
            /*
             * Cuando se pulse el botón enviar, pasaremos el texto del textBox a bytes utilizando Encoding. Estos datos se
             * guardan en un array de bytes.
             * Después llamamos al método Send, donde le pasaremos el array de bytes, su longitud y remoteep.
             * Remoteep, como dice el enunciado, es un IPEndPoint que representa un punto final de red como una dirección IP 
             * y un número de
                puerto. En el caso servidor no se tiene que unir a ningún otro servidor, la referencia es
                nula.
             */
        }

        //Método que envía los argumentos pasados mediante .Send
        public int Send(byte[] a,int b,IPEndPoint c){

           return udpclient.Send(a, b, c);
        }
    }
}
