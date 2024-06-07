using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Collections;
using Mysqlx.Resultset;

namespace DatabaseConnector{
    public class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        private void Initialize()
        {
            server = "localhost";
            database = "librarymanagement";
            uid = "root";
            password = "";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + 
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        private bool OpenConnection()
        {
            try{
                connection.Open();
                return true;
        }
        catch (MySqlException ex)
        {
            //When handling errors, you can your application's response based 
            //on the error number.
            //The two most common error numbers when connecting are as follows:
            //0: Cannot connect to server.
            //1045: Invalid user name and/or password.
            switch (ex.Number)
            {
                case 0:
                    Console.WriteLine("Error");
                    break;

                case 1045:
                    Console.WriteLine("Invalid username/password, please try again");
                    break;
            }
            return false;
        }
        }

        //Close connection
        private bool CloseConnection()
        {
            try{
                connection.Close();
                return true;
        }
        catch (MySqlException ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
        }

        //Insert statement
        public void Insert(string query)
        {
            if (this.OpenConnection() == true){
                MySqlCommand mySqlCommand= new MySqlCommand(query, connection);
                mySqlCommand.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update()
        {
        }

        //Delete statement
        public void Delete()
        {
        }

        //Select statement
    public Dictionary<int, Dictionary<string, string>> Select(string query, List<string> searchFields)
    {
        //Create a list to store the result
        Dictionary<int, Dictionary<string, string>> data = new Dictionary<int, Dictionary<string, string>>();
        Dictionary<string, string> entries = new Dictionary<string, string>();

            //Open connection
            if (this.OpenConnection() == true){
            //Create Command
                MySqlCommand mySqlCommand = new MySqlCommand(query, connection);

                //Create a data reader and Execute the command
                MySqlDataReader dataReader = mySqlCommand.ExecuteReader();
                

                //Read the data and store them in the list
                    int i = 0;
                    while (dataReader.Read()){
                        data.Add(i, new Dictionary<string, string>());
                        foreach (var item in searchFields)
                        {   
                            data[i].Add(item, dataReader[item].ToString());
                        }
                        i++;
                    }
                    
                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return data;
            }
        else
        {
            return data;
        }
    }

    }

}