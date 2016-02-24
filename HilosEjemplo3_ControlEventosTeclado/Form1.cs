using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;

namespace HilosEjemplo3_ControlEventosTeclado
{
    public partial class Form1 : Form
    {
        /*Objeto para enumerar, determinara la posicion 
         * que tiene el objeto actualmente*/
        enum Position
        {
            left, right, up, down
        }


        /*Posicion del elemento*/
        private int posx;
        private int posy;

        private int ancho;
        private int alto;

        /*Indica el cambio que tendra el elemento*/
        private int cambio;


        Thread hilo;

        /*Variable posicion creada a partir del enum*/
        private Position posicion;


        public Form1()
        {
            InitializeComponent();

            posx = 50;
            posy = 50;
            cambio = 5;

            ancho = 50;
            alto = 50;

            posicion = Position.down;
            hilo = new Thread(repintar);
            hilo.Start();
        }


        /*Esta funcion se genero buscando la funcion PAINT de la interfaz,*
          dandole en el rayo de las propiedades*/
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*Cada ves que se refresque la pantalla, actualiza la posicion*/
            e.Graphics.FillRectangle(Brushes.Blue, posx, posy, ancho, alto);


            /*Recordar cambiar la propiedad de la imagen a cambiar si es
             posterior para que pueda refrescar la imagen, sin esto no la 
             pinta*/
            //e.Graphics.DrawImage(new Bitmap("hongo.png"), posx, posy, ancho, alto);
        }

        public void repintar()
        {
            while (true)
            {

                Console.Write("(" + this.Height + "-" + posy + ")");

                /*Si la posicion es izquiera, cada vez que se ejecuta el hilo se mueve
                 a la izquierda*/
                if (posicion == Position.left)
                {
                    if (posx > 0)
                    {
                        posx = posx - cambio;
                    }
                }

                if (posicion == Position.right)
                {
                    if (this.Width > posx + ancho)
                    //if (this.Width-cambio-ancho > posx)
                    {
                        posx = posx + cambio;
                    }

                }

                if (posicion == Position.up)
                {
                    if (posy > 0)
                    {
                        posy = posy - cambio;
                    }

                }

                if (posicion == Position.down)
                {
                    if (this.Height > posy + alto)
                    {
                        //if(this.Height-cambio-(alto/2) > (posy + alto + cambio) ){
                        posy = posy + cambio;
                    }

                }

                //Repinta la interfaz
                Invalidate();

                //Duerme el hilo
                Thread.Sleep(100);
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            /*Si precionaros izquierda en el teclado, actualiza la posicion a la izquierda*/
            if (e.KeyCode == Keys.Left)
            {
                posicion = Position.left;
            }

            if (e.KeyCode == Keys.Right)
            {
                posicion = Position.right;
            }

            if (e.KeyCode == Keys.Up)
            {
                posicion = Position.up;
            }

            if (e.KeyCode == Keys.Down)
            {
                posicion = Position.down;
            }
        }


    }
}
