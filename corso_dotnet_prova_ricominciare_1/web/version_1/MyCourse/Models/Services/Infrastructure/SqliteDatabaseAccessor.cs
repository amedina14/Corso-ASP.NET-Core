using System;
using System.Data;
using Microsoft.Data.Sqlite;
using MyCourse.Models.Services.Application;

namespace MyCourse.Models.Services.Infrastructure{

    public class SqliteDatabaseAccessor : IDatabaseAccessor{

        public DataSet Query(string query){

            /* 
                Il blocco using serve a distruggere gli oggetti correttamente (come la connessione),
                anche quando si verifica un'eccezione, laddove si implementi una classe IDisposable.
            */
            using(var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                conn.Open();
                using(var cmd = new SqliteCommand(query, conn))
                {
                    // A questo punto abbiamo trovato i risultati. Ora li leggiamo uno ad uno.
                    using(SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        // E li passiamo alle classi disconnesse: DataSet, DataTable.
                        // Crea DataSet
                        var dataSet = new DataSet();

                        //dataSet.EnforceConstraints = false;

                        // Crea DataTable
                        var dataTable = new DataTable();

                        // Aggiunge la tabella al set
                        dataSet.Tables.Add(dataTable);

                        /*
                        while(reader.Read()){
                            string title = (string) reader["Title"]; 
                        }
                        */

                        // Legge i registri
                        dataTable.Load(reader);

                        return dataSet;
                    }
                }                
            }
            /*
                Attenzione: Usando conn.Dispose(); 
                Se si produce un errore la connessione resta aperta.
            */
        }
    }
}