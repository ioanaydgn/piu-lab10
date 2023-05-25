using LibrarieModele;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using LibrarieModele;
using LibrarieModele.Enumerari;
using NivelStocareDate;
using System.Collections;

namespace EvidentaStudenti_UI_WindowsForms
{
    public partial class Form2 : Form
    {


        IStocareData adminStudenti;

        private Label lblHeaderNume;
        private Label lblHeaderPrenume;
        private Label lblHeaderNote;

        private Label[] lblsNume;
        private Label[] lblsPrenume;
        private Label[] lblsNote;

        private const int LATIME_CONTROL = 100;
        private const int DIMENSIUNE_PAS_Y = 30;
        private const int DIMENSIUNE_PAS_X = 120;
        private const int OFFSET_X = 600;

        ArrayList disciplineSelectate = new ArrayList();

        private List<Student> studenti;

        public Form2()
        {

            adminStudenti = StocareFactory.GetAdministratorStocare();

            InitializeComponent();

            //setare proprietati
            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(100, 100);
            this.Font = new Font("Arial", 9, FontStyle.Bold);
            this.ForeColor = Color.LimeGreen;
            this.Text = "Informatii studenti";
        }
        public void AfisareStudentiInControlListbox(List<Student> studenti)
        {
            lstAfisare.Items.Clear();
            foreach (Student student in studenti)
            {
                //pentru a adauga un obiect de tip Student in colectia de Items a unui control de tip ListBox, 
                // clasa Student trebuie sa implementeze metoda ToString() specificand cuvantul cheie 'override' in definitie
                //pentru a arata ca metoda ToString a clasei de baza (Object) este suprascrisa
                lstAfisare.Items.Add(student);

                //personalizare sursa de date
                //lstAfisare.Items.Add(s.NumeComplet);
            }
        }
        public void AfiseazaStudenti(List<Student> studenti)
        {
            //adaugare control de tip Label pentru 'Nume';
            lblHeaderNume = new Label();
            lblHeaderNume.Width = LATIME_CONTROL;
            lblHeaderNume.Text = "Nume";
            lblHeaderNume.Left = OFFSET_X + 0;
            lblHeaderNume.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderNume);

            //adaugare control de tip Label pentru 'Prenume';
            lblHeaderPrenume = new Label();
            lblHeaderPrenume.Width = LATIME_CONTROL;
            lblHeaderPrenume.Text = "Prenume";
            lblHeaderPrenume.Left = OFFSET_X + DIMENSIUNE_PAS_X;
            lblHeaderPrenume.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderPrenume);

            //adaugare control de tip Label pentru 'Note';
            lblHeaderNote = new Label();
            lblHeaderNote.Width = LATIME_CONTROL;
            lblHeaderNote.Text = "Note";
            lblHeaderNote.Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
            lblHeaderNote.ForeColor = Color.DarkGreen;
            this.Controls.Add(lblHeaderNote);

            int nrStudenti = studenti.Count;
            lblsNume = new Label[nrStudenti];
            lblsPrenume = new Label[nrStudenti];
            lblsNote = new Label[nrStudenti];

            int i = 0;
            foreach (Student student in studenti)
            {
                //adaugare control de tip Label pentru numele studentilor;
                lblsNume[i] = new Label();
                lblsNume[i].Width = LATIME_CONTROL;
                lblsNume[i].Text = student.Nume;
                lblsNume[i].Left = OFFSET_X + 0;
                lblsNume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNume[i]);

                //adaugare control de tip Label pentru prenumele studentilor
                lblsPrenume[i] = new Label();
                lblsPrenume[i].Width = LATIME_CONTROL;
                lblsPrenume[i].Text = student.Prenume;
                lblsPrenume[i].Left = OFFSET_X + DIMENSIUNE_PAS_X;
                lblsPrenume[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsPrenume[i]);

                //adaugare control de tip Label pentru notele studentilor
                lblsNote[i] = new Label();
                lblsNote[i].Width = LATIME_CONTROL;
                lblsNote[i].Text = string.Join(" ", student.GetNote());
                lblsNote[i].Left = OFFSET_X + 2 * DIMENSIUNE_PAS_X;
                lblsNote[i].Top = (i + 1) * DIMENSIUNE_PAS_Y;
                this.Controls.Add(lblsNote[i]);
                i++;
            }
        }

        public Form2(List<Student> studenti)
        {
            InitializeComponent();

            this.studenti = studenti;

            // Restul codului constructorului Form2
        }

        // Restul codului clasei Form2
    }

}
