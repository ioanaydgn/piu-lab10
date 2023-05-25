﻿using LibrarieModele.Enumerari;
using System;
using System.Collections;
using System.Linq;

namespace LibrarieModele
{
    [Serializable]
    public class Student
    {
        //constante
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';
        private const char SEPARATOR_SECUNDAR_FISIER = ' ';
        private const bool SUCCES = true;
        public const int NOTA_MINIMA = 1;
        public const int NOTA_MAXIMA = 10;

        private const int ID = 0;
        private const int NUME = 1;
        private const int PRENUME = 2;
        private const int SPECIALIZARE = 3;
        private const int DISCIPLINE = 4;
        private const int NOTE = 5;

        // data membra privata
        int[] note;

        //proprietati auto-implemented
        public int IdStudent { get; set; } //identificator unic student
        public string Nume { get; set; }
        public string Prenume { get; set; }

        public ProgramStudiu Specializare { get; set; }
        public ArrayList Discipline { get; set; }

        public string DisciplineAsString
        {
            get
            {
                return string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), Discipline.ToArray());
            }
        }

        public int[] GetNote()
        {
            // returneaza o copie a vectorului, astfel încât utilizatorii acestei 
            // clase să nu poata modifica în mod direct conținutul vectorului 
            return (int[])note.Clone();
        }

        //contructor implicit
        public Student()
        {
            Nume = Prenume = string.Empty;
        }

        //constructor cu parametri
        public Student(int idStudent, string nume, string prenume)
        {
            this.IdStudent = idStudent;
            this.Nume = nume;
            this.Prenume = prenume;
        }

        //constructor cu un singur parametru de tip string care reprezinta o linie dintr-un fisier text
        public Student(string linieFisier)
        {
            string[] dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            IdStudent = Convert.ToInt32(dateFisier[ID]);
            Nume = dateFisier[NUME];
            Prenume = dateFisier[PRENUME];
            SetNote(dateFisier[NOTE], SEPARATOR_SECUNDAR_FISIER);

            Specializare = (ProgramStudiu)Enum.Parse(typeof(ProgramStudiu), dateFisier[SPECIALIZARE]);
            Discipline = new ArrayList();
            //adauga mai multe elemente in lista de discipline
            Discipline.AddRange(dateFisier[DISCIPLINE].Split(SEPARATOR_SECUNDAR_FISIER));

        }

        public float Media
        {
            get
            {
                float media = 0;
                foreach (int nota in note)
                {
                    media += nota;
                }

				media = media / note.Length;

                return media;
            }
        }

        public string Info()
        {
            string info = string.Format("Id:{0} Nume:{1} Prenume: {2}",
                IdStudent.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Prenume ?? " NECUNOSCUT "));

            return info;
        }

        public string ConversieLaSir_PentruFisier()
        {
            string sNote = string.Empty;
            if (note != null)
            {
                sNote = string.Join(SEPARATOR_SECUNDAR_FISIER.ToString(), note);
            }

            string obiectStudentPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}{5}{0}{6}",
                SEPARATOR_PRINCIPAL_FISIER,
                IdStudent.ToString(),
                (Nume ?? " NECUNOSCUT "),
                (Prenume ?? " NECUNOSCUT "),
                Specializare,
                DisciplineAsString,
                sNote);

            return obiectStudentPentruFisier;
        }

        public void SetNote(string sirNote, char delimitator = ' ')
        {
            string[] vectorNoteDupaSplit = sirNote.Split(delimitator);
            note = new int[vectorNoteDupaSplit.Length];

            int nrNote = 0;
            foreach (string nota in vectorNoteDupaSplit)
            {
                bool rezultatConversie = Int32.TryParse(nota, out note[nrNote]);
                if (rezultatConversie == SUCCES && ValideazaNota(note[nrNote]) == SUCCES)
                {
                    nrNote++;
                }
            }

            Array.Resize(ref note, nrNote);
        }

        public void SetNote(int[] _note)
        {
            note = new int[_note.Length];
            _note.CopyTo(note, 0);
        }

        public override string ToString()
        {
            return ConversieLaSir_PentruFisier();
        }

        private bool ValideazaNota(int nota)
        {
            if (nota >= NOTA_MINIMA && nota <= NOTA_MAXIMA)
            {
                return true;
            }

            return false;
        }
    }
}
