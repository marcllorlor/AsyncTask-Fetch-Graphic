using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;
using static practicaPowerAPI.ClDadesGeneracioElectricitat;

namespace practicaPowerAPI
{
    public partial class fmGrafica : Form
    {
        //Declaracio de variables globals
        
        //Aqui decalrem la array dels 12 mesos del any
        string[] mesos = new string[12] { "Gener", "Febrer", "Març", "Abril", "Maig", "Juny", "Juliol", "Agost", "Setembre", "Octubre", "Novembre", "Desembre" };
        public fmGrafica()
        {
            InitializeComponent();
        }

        //Sincerament no tinc ni idea de per que fem servir el async i el await, pero si no el posem no funciona
        private async void fmGrafica_Load(object sender, EventArgs e)
        {
            //Cada cop que s'arranqui el programa executara aquesta funcio
            funcioEsperaGrafiques();

            //Aqui estem declarant la TASCA asyncrona de la funcio que ens retorna les dades de la API
            Task TascaConsultaPreuElectricitat = ConsultarPreuElectricitat();
            //El await és perque esperi a que acabi la tasca per a continuar //Indirectament aixo es una funcio syncrona, fins que no s'acaba no continua
            await TascaConsultaPreuElectricitat;

            //Diem que l'any que ha de triar sempre és l'ultim de la llista del combobox
            cbAnysGrafica.SelectedIndex = cbAnysGrafica.Items.Count-1;
            
            
            //Tornem a fer una TASKa asyncrona
            Task TascaConsultaGeneracioElectricitat = ConsultarGeneracioElectricitat();
            await TascaConsultaGeneracioElectricitat;

            //I ara cridem a la funcio de mostrar grafiques
            mostrarGrafiques();

        }

        

        private async Task ConsultarGeneracioElectricitat()
        {
            //Pillem el link final de la generacio electrica //Aquest link és el que conte l'any que hem triat al combobox, si no hem triat res pillara l'ultim any
            string linkfinal = retornarLinkFinal();
            HttpClient client = new HttpClient();               // el client és qui fa efectives les peticions a un servidor web a través d'una URL http/https
            HttpRequestMessage peticio = new HttpRequestMessage // l'HttpRequestMessage és qui defineix la URL i les capçaleres de la petició
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri(linkfinal)
            };

            //Quan acabi de fer la peticio entrara a dins d'aquest using
            using (var response = await client.SendAsync(peticio))          // la utilització de la clàusula using fa que l'alliberament dels recursos 
            {
                //Aqui fem la comprovacio de que el status de la peticio es OK   // del sistema utilitzats per a fer la petició sigui més eficient
                //Si la peticio es incorrecte o el servidor no funciona del tot be petara aqui
                response.EnsureSuccessStatusCode();                         // A .NET el tema de l'alliberament de recursos quan els objectes deixen
                var body = await response.Content.ReadAsStringAsync();      // d'existir no és senzill perquè hi ha un agent, el GC (Garbage Collector),
                RootGeneracio dadesJson = JsonConvert.DeserializeObject<RootGeneracio>(body); //Aqui ya tenim totes les dades a dins de json
                //Aqui enviem el json a la funcio que ens modificara els valors dels grafic
                canviarinformaciosegongrafic(dadesJson);

            }

            

        }

        private void canviarinformaciosegongrafic(RootGeneracio dadesJson)
        {
            //Aqui declarem la variable que farem servir per comprovar si el text esta be

            chGeneracioElectrica.Series.Clear();
            //Aqui fem un bucle per recorrer tots els tipus de generacio d'energia
            foreach (Included s in dadesJson.included)
            {
                if (s.attributes.values.Count == 12)                // hi ha sèries que no tenen informació de tots els mesos i no surten zeros sinó que hi ha menys nodes al JSON
                {                                                   // el Chart de tipus StackedColumn obliga a tenir el mateix número d'elements en cada sèrie            
                    if (s.attributes.title != "Generación total")
                    {
                        chGeneracioElectrica.Series.Add(new Series(s.attributes.title));
                        chGeneracioElectrica.Series[chGeneracioElectrica.Series.Count - 1].ChartType = SeriesChartType.StackedColumn100; //Ens assegurem que el tipus de grafic és StackedColumn100
                        chGeneracioElectrica.Series[chGeneracioElectrica.Series.Count - 1].Points.Clear();
                        foreach (Value v in s.attributes.values)
                        {
                            //Afegim el valor que li pertany a cada mes
                            chGeneracioElectrica.Series[chGeneracioElectrica.Series.Count - 1].Points.AddXY(mesos[v.datetime.Month - 1], Math.Round(v.percentage, 2));
                            //I afegim la etiqueta hover que s'activara  quan passem el ratoli per sobre de la grafica i ens mostrara el valor que te la etiqueta
                            chGeneracioElectrica.Series[chGeneracioElectrica.Series.Count - 1].Points[chGeneracioElectrica.Series[chGeneracioElectrica.Series.Count - 1].Points.Count - 1].ToolTip = s.attributes.title + Environment.NewLine + v.value.ToString() + " Mwh" + Environment.NewLine + " (" + Math.Round(v.percentage, 2) * 100 + "%)";
                        }
                    }
                }
            }
        }

        private string retornarLinkFinal()
        {
            //Pillem l'any que tenim triar a dins del combobox
            string anyfinal = cbAnysGrafica.Text;

            //Ajuntem l'any amb la resta de la url
            string linkfinal = "https://apidatos.ree.es/es/datos/generacion/estructura-generacion?&geo_trunc=electric_system&geo_limit=ccaa&geo_ids=7&time_trunc=month&start_date=" + anyfinal + "-01-01T00:00&end_date=" + anyfinal + "-12-31T23:59";
            //I retornem el link
            return linkfinal;
        }

        private void mostrarGrafiques()
        {
            //Mostrem el groupbox de les grafiques i fem que gif es deixi de mostrar
            gpTotal.Visible = true;
            funcioMostrarDespareixerGIF(false);
            //Tambe deixem el ratoli al ratoli default
            Cursor = Cursors.Default;
        }

        private void funcioEsperaGrafiques()
        {
            //El que fem en aquestes funcions es deixar de mostrar els dos grafics, posem el ratoli en espera i mostrem el GIF,ja que es una funcio que li pasem un bool
            gpTotal.Size = new Size(this.Height - 40, this.Width - 40);
            gpTotal.Visible = false;
            gpTotal.Size = new Size(this.Width - 50, this.Height - 100);
            Cursor = Cursors.WaitCursor;
            //Posem el gif de fons a true
            funcioMostrarDespareixerGIF(true);
        }

        private void funcioMostrarDespareixerGIF(bool v)
        {
            //Aquesta es la funcio que fa que es mostri o no el gif
            if (v) //La variable "v" és la variable que li passem per parametre, si es true mostra el gif si no, no
            {
                //Com que no sabem quina mida tindra el gif ni a on merdes estara, li donem la mida i la posicio sempre
                pbGIFEspera.Size = new Size(300, 300);
                pbGIFEspera.Location = new Point((this.Width - pbGIFEspera.Width) / 2, (this.Height - pbGIFEspera.Height) / 2);
                pbGIFEspera.Visible = true;
            }
            else
            {
                //Si rep false, el gif es deixara de mostrar
                pbGIFEspera.Visible = false;
            }
        }

        
        private async Task ConsultarPreuElectricitat()
        {
            HttpClient client = new HttpClient();               // el client és qui fa efectives les peticions a un servidor web a través d'una URL http/https
            HttpRequestMessage peticio = new HttpRequestMessage // l'HttpRequestMessage és qui defineix la URL i les capçaleres de la petició
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://api.preciodelaluz.org/v1/prices/all?zone=PCB"),
            };


            using (var response = await client.SendAsync(peticio))          // la utilització de la clàusula using fa que l'alliberament dels recursos 
            {                                                               // del sistema utilitzats per a fer la petició sigui més eficient
                response.EnsureSuccessStatusCode();                         // A .NET el tema de l'alliberament de recursos quan els objectes deixen
                var body = await response.Content.ReadAsStringAsync();      // d'existir no és senzill perquè hi ha un agent, el GC (Garbage Collector),
                
                Root dadesJson = JsonConvert.DeserializeObject<Root>(body); //Aqui ya tenim totes les dades a dins de json
                canviarinfoprimergrafic(dadesJson);
                
            }

            //Aqui farem la segona consulta per canviar el nom del preu
            
        }

        private void canviarinfoprimergrafic(Root dadesJson)
        {
            //Aqui posem els valors a cada punt
            chPreuMwHDurantAvui.Series[0].IsValueShownAsLabel = true;

            //Fem que el interval de la X (Barra d'abaix sigui nomes 1, aixi no es va saltant res)
            chPreuMwHDurantAvui.ChartAreas[0].AxisX.Interval = 1;

            //Aqui canviarem el valor min de la grafica
            crearArrayValors(dadesJson);
            

            //Aquesta sera la funcio que farem quan volguem canviar els valors de la grafica
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("00-01", dadesJson._0001.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("01-02", dadesJson._0102.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("02-03", dadesJson._0203.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("03-04", dadesJson._0304.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("04-05", dadesJson._0405.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("05-06", dadesJson._0506.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("06-07", dadesJson._0607.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("07-08", dadesJson._0708.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("08-09", dadesJson._0809.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("09-10", dadesJson._0910.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("10-11", dadesJson._1011.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("11-12", dadesJson._1112.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("12-13", dadesJson._1213.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("13-14", dadesJson._1314.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("14-15", dadesJson._1415.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("15-16", dadesJson._1516.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("16-17", dadesJson._1617.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("17-18", dadesJson._1718.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("18-19", dadesJson._1819.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("19-20", dadesJson._1920.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("20-21", dadesJson._2021.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("21-22", dadesJson._2122.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("22-23", dadesJson._2223.price);
            chPreuMwHDurantAvui.Series[chPreuMwHDurantAvui.Series.Count() - 1].Points.AddXY("23-24", dadesJson._2324.price);
        }

        private void crearArrayValors(Root dadesJson)
        {
            //Creo una array de numeros per despres guardar el preu de la electricitat 
            Double[] arraynumeros = new Double[]
            {
                dadesJson._0001.price,
                dadesJson._0102.price,
                dadesJson._0203.price,
                dadesJson._0304.price,
                dadesJson._0405.price,
                dadesJson._0506.price,
                dadesJson._0607.price,
                dadesJson._0708.price,
                dadesJson._0809.price,
                dadesJson._0910.price,
                dadesJson._1011.price,
                dadesJson._1112.price,
                dadesJson._1213.price,
                dadesJson._1314.price,
                dadesJson._1415.price,
                dadesJson._1516.price,
                dadesJson._1617.price,
                dadesJson._1718.price,
                dadesJson._1819.price,
                dadesJson._1920.price,
                dadesJson._2021.price,
                dadesJson._2122.price,
                dadesJson._2223.price,
                dadesJson._2324.price
            };
            //Aqui estem fent la mitjana
            lbPreuMitja.Text = "El preu mitja d'avui és: " + Math.Round(arraynumeros.Average()).ToString();
            //Aqui estem canviant el valor minim del eix Y de la grafica
            chPreuMwHDurantAvui.ChartAreas[0].AxisY.Minimum = arraynumeros.Min() - 10;

            //Aqui cridarem la funcio de la hora actual
            posarlahora(arraynumeros);
        }

        private void posarlahora(double[] arraynumeros)
        {
            //Aqui sabrem la hora que es actualment i depenent d'aixo posarem un valor o un altre
            double preuaramateix = 0;
            var hora = DateTime.Now.Hour;
            preuaramateix = arraynumeros[hora];
            lbPreuAraMateix.Text = "Preu ara mateix: " + preuaramateix.ToString();
        }

        private async void cbAnysGrafica_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Aqui es a on farem la consulta un altre cop cada cop que es canvii l'index de la merda de generacio electrica
            funcioEsperaGrafiques();
            Task TascaConsultaGeneracioElectricitat2 = ConsultarGeneracioElectricitat();
            await TascaConsultaGeneracioElectricitat2;
            mostrarGrafiques();
        }
    }
}
