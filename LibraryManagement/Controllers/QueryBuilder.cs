using DatabaseConnector;
using Models;

namespace QueryBuilder {
    public class BuildQuery {
        private string query;
        private List<string> SearchFields;
        private bool hasWhereStatement;
        private bool hasSelectStatement;
        private bool hasOrderByStatement;

        public BuildQuery() {
            this.query = "";
            this.hasWhereStatement = false;
            this.hasSelectStatement = false;
            this.hasOrderByStatement = false;
            this.SearchFields = new List<string>();
        }
        public BuildQuery Select(string table, params string[] args){
            if (args[0] != "*"){
                this.query =  "SELECT " + string.Join(",", args) + " FROM " + table;
                foreach (var item in args)
                {
                    this.SearchFields.Add(item);
                    
                }

            }else {
                this.query = "SELECT " + args[0] + " FROM " + table;
            }
            this.hasSelectStatement = true;

            return this;
        }

        public BuildQuery Where(string column, string optr, string comparison) {
            if (!hasWhereStatement && hasSelectStatement) {
                this.query += " WHERE " + column + optr + comparison;
                this.hasWhereStatement = true;
            }else if (hasWhereStatement){
                this.query += " AND " + column + optr + comparison;
            }else {
                return this;
            }

            return this;
        }

        public BuildQuery WhereIn(string column, params string[] args){
            string comparison = '(' + string.Join(",", args) + ')';
            this.query += Where(column, " IN " , comparison);

            return this;
        }

        public BuildQuery OrderBy(string order = "ASC", params string[] columns){
            if (hasSelectStatement && !hasOrderByStatement){
                this.query += " ORDER BY " + string.Join(",", columns)+ " " + order;
                this.hasOrderByStatement = true;
            }else if (hasSelectStatement && hasOrderByStatement){
                this.query += ", " + string.Join (",", columns)+ " " + order;
            }else{
                return this;
            }

            return this;
        }

        public Dictionary<int, Dictionary<string,string>> Get() {
            DatabaseConnector.DBConnect db = new DBConnect();
            Dictionary<int, Dictionary<string,string>> data = db.Select(this.query, this.SearchFields);
            
            return data;
        }
    }
}