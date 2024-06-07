using QueryBuilder;

namespace Models {
    public class Model: BuildQuery{
        private string primaryKey;
        private string foreignKey;
        private bool hasForeignKey;
        private List<string> tableSearchableFields;

        public Model(){
            this.hasForeignKey = false;
            this.primaryKey = "";
            this.foreignKey = "";
            this.tableSearchableFields = new List<string>();
        }

        public string GetPrimaryKey(){
            return this.primaryKey;
        }

        public void SetPrimaryKey(string primaryKey){
            this.primaryKey = primaryKey;
        }

        public void SetForeignKey(string foreignKey) {
            this.foreignKey = foreignKey;
            this.hasForeignKey = true;
        }
        public List<string> GetTableSearchableFields(){
            return this.tableSearchableFields;
        }
        public void SetTableSearchableFields(params string[] fields){
            foreach (var item in fields)
            {
                this.tableSearchableFields.Add(item);
            }
        }
    }

}