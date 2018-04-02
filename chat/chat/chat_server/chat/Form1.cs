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


namespace chat
{
    public partial class Form1 : Form
    {


        UdpClient udpclient;
        IPEndPoint remote;


        public Form1()
        {
            InitializeComponent();
            udpclient = new UdpClient(8080);
            IPAddress multicastaddress = IPAddress.Parse("224.0.0.1");
            udpclient.JoinMulticastGroup(multicastaddress);
            remote = null;

            /*
             *remote es un IPEndPoint representa un punto final de red como una dirección IP y un número de
            puerto. En el caso servidor no se tiene que unir a ningún otro servidor, la referencia es
            nula.
             */

            Thread t = new Thread(new ThreadStart(MyThreadMethod));
            t.Start();
            CheckForIllegalCrossThreadCalls = false;
        }

        
        /*
         * Se pone en un hilo, ya que el enunciado dice que el servidor queda bloqueado cada vez que no se le envía
         * datos. Como el cliente envía bytes, pasa los bytes a String para poder mostrarlo en el Form.
         */ 

         private void MyThreadMethod ()
        {
            while (true)
            {

                // Blocks until a message returns on this socket from a remote host.
                Byte[] receiveBytes = udpclient.Receive(ref remote);
                //Pasamos a String los bytes recibidos.
                string returnData = Encoding.Unicode.GetString(receiveBytes);
                textBox1.Text = returnData;
                listBox1.Items.Add(returnData);


            }
        }


     


            


            

        


        

            
           



            

           

//try catch de recibir bytes
         /*   try
            {
                


            }

            catch(Exception e)
            {
                Console.WriteLine(e.ToString()); 
            } */

            
        
 


    }
}
