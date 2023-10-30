﻿using Microsoft.Win32;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static Catalogo_de_Animes.Form1;
using System.Collections.Generic;

namespace Catalogo_de_Animes
{
    public partial class Form1 : Form
    {
        private List<Anime> Animes;

        public Form1()
        {
            InitializeComponent();
            AtualizarDataGridView();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=SETH;Initial Catalog=Animes;Integrated Security=True"))
                {
                    con.Open();

                    string query = "INSERT INTO Anime ( Id, Anime, Genero, Episodio) VALUES ( @Id, @Anime, @Genero, @Episodio)";
                    SqlCommand command = new SqlCommand(query, con);

                    command.Parameters.AddWithValue("@Id", int.Parse(txtId.Text));
                    command.Parameters.AddWithValue("@Anime", txtanime.Text);
                    command.Parameters.AddWithValue("@Genero", boxgenero.Text);
                    command.Parameters.AddWithValue("@Episodio", txtepisodio.Text);

                    command.ExecuteNonQuery();
                    MessageBox.Show("Adicionado com Sucesso");
                    AtualizarDataGridView();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao adicionar: " + ex.Message);
            }
        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);

                    using (SqlConnection con = new SqlConnection("Data Source=SETH;Initial Catalog=Animes;Integrated Security=True"))
                    {
                        con.Open();

                        string query = "DELETE FROM Anime WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, con);
                        command.Parameters.AddWithValue("@Id", selectedId);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registro excluído com sucesso!");

                            AtualizarDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Nenhum registro foi excluído.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um registro para excluir.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao excluir o registro: " + ex.Message);
            }
        }

 
        private void btnEdit_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    int selectedId = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["ID"].Value);


                    string novoAnime = txtanime.Text;
                    string novoGenero = boxgenero.Text;
                    string novoEpisodio = txtepisodio.Text;

                    using (SqlConnection con = new SqlConnection("Data Source=SETH;Initial Catalog=Animes;Integrated Security=True"))
                    {
                        con.Open();

                        string query = "UPDATE Anime SET Anime = @Anime, Genero = @Genero, Episodio = @Episodio WHERE Id = @Id";
                        SqlCommand command = new SqlCommand(query, con);

                        command.Parameters.AddWithValue("@Id", selectedId);
                        command.Parameters.AddWithValue("@Anime", novoAnime);
                        command.Parameters.AddWithValue("@Genero", novoGenero);
                        command.Parameters.AddWithValue("@Episodio", novoEpisodio);

                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Registro atualizado com sucesso!");

                            AtualizarDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("Nenhum registro foi atualizado.");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Selecione um registro para editar.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao editar o registro: " + ex.Message);
            }
        }

        private void AtualizarDataGridView()
        {
            try
            {
                using (SqlConnection con = new SqlConnection("Data Source=SETH;Initial Catalog=Animes;Integrated Security=True"))
                {
                    con.Open();

                    string query = "SELECT * FROM Anime";
                    SqlCommand command = new SqlCommand(query, con);

                    DataTable dataTable = new DataTable();

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao atualizar o DataGridView: " + ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class Anime
        {
            public string ID { get; set; }
            public string AnimeName { get; set; }
            public string Genero { get; set; }
            public string Episodio { get; set; }

            public Anime(string id, string animeName, string genero, string episodio)
            {
                ID = id;
                AnimeName = animeName;
                Genero = genero;
                Episodio = episodio;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];

                txtId.Text = row.Cells["Id"].Value.ToString();
                txtanime.Text = row.Cells["Anime"].Value.ToString();
                boxgenero.Text = row.Cells["Genero"].Value.ToString();
                txtepisodio.Text = row.Cells["Episodio"].Value.ToString();
            }
        }
    }
}
