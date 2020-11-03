using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;
using MyCourse.Models.Services.Application;

namespace MyCourse.Models.Services.Infrastructure{

    public class SqliteDatabaseAccessor : IDatabaseAccessor{

        public DataSet Query(FormattableString formattableQuery){

            // prevenire Sql Injection
            /*
                1. Si prendono gli argomenti INTERPOLATI della query.
                2. Si crea la lista per i parametri sqlite.
                3. Si crea il parametro sqlite in base al numero di argomenti,
                4. E si aggiunge alla lista.
                5. Riscrive ed antepone @ per ogni argomento.
                6. Infine converte l'intera query a stringa.
            */
            var queryArguments = formattableQuery.GetArguments();
            var sqliteParameters = new List<SqliteParameter>();
            for(var i = 0; i < queryArguments.Length; i++)
            {
                var parameter = new SqliteParameter(i.ToString(), queryArguments[i]);
                sqliteParameters.Add(parameter);
                queryArguments[i] = "@" + i;
            }
            // query formattata a string
            string query = formattableQuery.ToString();

            /* 
                Il blocco using serve a distruggere gli oggetti correttamente (come la connessione),
                anche quando si verifica un'eccezione, laddove si implementi una classe IDisposable.
            */
            using(var conn = new SqliteConnection("Data Source=Data/MyCourse.db"))
            {
                conn.Open();
                using(var cmd = new SqliteCommand(query, conn))
                {
                    // Aggiunge tutti i SqliteParameters al SqliteCommand.
                    cmd.Parameters.AddRange(sqliteParameters);

                    // A questo punto abbiamo trovato i risultati. Ora li leggiamo uno ad uno.
                    using(SqliteDataReader reader = cmd.ExecuteReader())
                    {
                        // E li passiamo alle classi disconnesse: DataSet, DataTable.
                        // Crea DataSet
                        var dataSet = new DataSet();

                        //dataSet.EnforceConstraints = false;

                        do{
                            // Crea N DataTable se ci sono più tabelle da leggere
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

                        }while(!reader.IsClosed);

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