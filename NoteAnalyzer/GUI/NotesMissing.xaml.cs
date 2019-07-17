using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NoteAnalyzer.GUI
{
    /// <summary>
    /// Interaction logic for NotesMissing.xaml
    /// </summary>
    public partial class NotesMissing : Window
    {
        public NotesMissing(List<int> notesFaltantes, List<int> notesInutilizados, List<int> notesCanceladas)
        {
            InitializeComponent();
            List<int> resumo = notesFaltantes;
            foreach (int i in notesInutilizados)
            {
                resumo.Remove(i);
            }
            foreach (int i in notesCanceladas)
            {
                resumo.Remove(i);
            }
            foreach (int n in notesFaltantes)
            {
                NotesFaltantesTextBox.Text = NotesFaltantesTextBox.Text + n + ",";
            }
            foreach (int n in notesInutilizados)
            {
                NotesInutilizadoTextBox.Text = NotesInutilizadoTextBox.Text + n + ",";
            }
            foreach (int n in notesCanceladas)
            {
                NotasCanceladasTextBox.Text = NotasCanceladasTextBox.Text + n + ",";
            }

            FaltantesLabel.Content = "Notas faltantes: " + notesFaltantes.Count;
            InutilizadasLabel.Content = "Notas Inutilizadas: " + notesInutilizados.Count;
            CanceladasLabel.Content = "Notas Canceladas: " + notesCanceladas.Count;
        }


    }
}
