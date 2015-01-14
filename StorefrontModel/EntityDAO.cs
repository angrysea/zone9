using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace StorefrontModel
{
    public class EntityDAO<T> where T : new()
    {
        private string[] Fields { get; set;  }
        private string TableName { get; set; }
        private string IndexName { get; set; }
        private T entity = default(T);

        public Entities Context { get; set; }

        public EntityDAO(Entities context, T entity, string indexName)
        {
            this.entity = entity;
            Type t = entity.GetType();
            this.Context = context;
            IndexName = indexName;
            TableName = t.Name;
            PropertyInfo[] propInfo = t.GetProperties();
            Fields = new string[propInfo.Length];
            for (Int32 i = 0; i <= Fields.Length - 1; i++)
            {
                Fields[i] = propInfo[i].Name;
            }
        }

        public T Entity
        {
            get
            {
                return entity;
            }

            set
            {
                entity = value;
            }
        }

        protected DbCommand getCommand(DbConnection con, string stmt, CommandType type)
        {
            DbCommand cmd = Context.dbFactory.CreateCommand();
            cmd.Connection = con;
            cmd.CommandText = stmt;
            cmd.CommandType = type;
            return cmd;
        }

        protected void AddParameter(DbCommand cmd, string name, Object value)
        {
            DbParameter param = Context.dbFactory.CreateParameter();
            param.ParameterName = name;
            if (value == null)
            {
                param.Value = "";
            }
            else
            {
                param.Value = value;
            }
            cmd.Parameters.Add(param);
        }

        protected void AddParameter(DbCommand cmd, string name, DbType type, int size, Object value)
        {
            DbParameter param = Context.dbFactory.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Size = size;
            if (value == null)
            {
                param.Value = "";
            }
            else
            {
                param.Value = value;
            }
            cmd.Parameters.Add(param);
        }

        protected void AddParameter(DbCommand cmd, string name, DbType type, int size)
        {
            DbParameter param = Context.dbFactory.CreateParameter();
            param.ParameterName = name;
            param.DbType = type;
            param.Size = size;
            cmd.Parameters.Add(param);
        }

        public DataSet select(string sql)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, sql, CommandType.Text);
                DbDataAdapter da = Context.dbFactory.CreateDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        public DataSet select(string sql, string parameter, object value)
        {
            StringBuilder builder = new StringBuilder(getSelectString());
            builder.Append(" where ");
            builder.Append(parameter);
            builder.Append(" = @");
            builder.Append(parameter);
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                AddParameter(cmd, "@" + parameter, value);
                DbDataAdapter da = Context.dbFactory.CreateDataAdapter();
                da.SelectCommand = cmd;
                DataSet ds = new DataSet();
                da.Fill(ds);

                return ds;
            }
        }

        public DataSet fill(DbCommand cmd)
        {
            DbDataAdapter da = Context.dbFactory.CreateDataAdapter();
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        protected string getSelectString() {
            return getSelectString(null);
        }

        protected string getSelectString(string where) {
            StringBuilder stmt = new StringBuilder("select ");

            for (int i = 0; i < Fields.Length; i++)
            {
                stmt.Append(Fields[i]);
                if (i + 1 < Fields.Length)
                {
                    stmt.Append(",");
                }
            }
            stmt.Append(" from ");
            stmt.Append(TableName);

            if (where != null) {
                stmt.Append(" where ");
                stmt.Append(where);
            }

            return stmt.ToString();
        }

        protected string getCountString(string where)
        {
            StringBuilder stmt = new StringBuilder("select count(*) from ");
            stmt.Append(TableName);

            if (where != null)
            {
                stmt.Append(" where ");
                stmt.Append(where);
            }

            return stmt.ToString();
        }

        protected string getCountByIDString()
        {
            StringBuilder stmt = new StringBuilder("select count(*) from ");
            stmt.Append(TableName);
            stmt.Append(" where ");
            stmt.Append(IndexName);
            stmt.Append(" = @");
            stmt.Append(IndexName);

            return stmt.ToString();
        }

        protected string getSelectByIDString()
        {
            StringBuilder stmt = new StringBuilder("select ");
            int size = Fields.Length;

            for (int i = 0; i < size; i++) {
                stmt.Append(Fields[i]);
                if (i + 1 < size) {
                    stmt.Append(",");
                }
            }
            stmt.Append(" from ");
            stmt.Append(TableName);
            stmt.Append(" where ");
            stmt.Append(IndexName);
            stmt.Append(" = @");
            stmt.Append(IndexName);

            return stmt.ToString();
        }

        protected string getInsertString() {
            StringBuilder stmt = new StringBuilder("insert into " + TableName + " (");

            for (int i = 0; i < Fields.Length; i++)
            {
                stmt.Append(Fields[i]);
                if (i + 1 < Fields.Length)
                {
                    stmt.Append(",");
                }
            }
            stmt.Append(") values (");
            for (int i = 0; i < Fields.Length; i++)
            {
                stmt.Append("@" + Fields[i]);
                if (i + 1 < Fields.Length)
                {
                    stmt.Append(",");
                }
            }
            stmt.Append(")");
            return stmt.ToString();
        }

        protected String getUpdateString() {
            StringBuilder astmt = new StringBuilder("update " + TableName + " set ");

            for (int i = 0; i < Fields.Length; i++)
            {
                if (IndexName.CompareTo(Fields[i]) != 0)
                {
                    astmt.Append(Fields[i]);
                    astmt.Append("=@");
                    astmt.Append(Fields[i]);
                    if (i + 1 < Fields.Length)
                    {
                        astmt.Append(",");
                    }
                }
            }
            astmt.Append(" where ");
            astmt.Append(IndexName);
            astmt.Append(" = @");
            astmt.Append(IndexName);
            return astmt.ToString();
        }

        protected string getDeleteString(String where)
        {
            StringBuilder stmt = new StringBuilder("delete from ");
            stmt.Append(TableName);

            if (where != null)
            {
                stmt.Append(" where ");
                stmt.Append(where);
            }

            return stmt.ToString();
        }

        protected string getDeleteString()
        {
            StringBuilder stmt = new StringBuilder("delete from ");
            stmt.Append(TableName);
            stmt.Append(" where ");
            stmt.Append(IndexName);
            stmt.Append(" = @");
            stmt.Append(IndexName);
            return stmt.ToString();
        }

        protected string getDeleteAllString()
        {
            StringBuilder stmt = new StringBuilder("delete from ");
            stmt.Append(TableName);
            return stmt.ToString();
        }

        public T First()
        {
            return First(entity);
        }

        public T First(T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getSelectString(), CommandType.Text);
                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = entity.GetType();

                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
            }
            return item;
        }

        public T First(string[] parameters, object[] values)
        {
            return First(parameters, values, entity);
        }

        public T First(string[] parameters, object[] values, T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                StringBuilder builder = new StringBuilder(getSelectString());
                builder.Append(" where ");
                for (Int32 i = 0; i < parameters.Length; i++)
                {
                    builder.Append(parameters[i]);
                    builder.Append(" = @");
                    builder.Append(parameters[i]);
                    if (i + 1 < parameters.Length)
                        builder.Append(" and ");
                }

                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                if (parameters != null)
                {
                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        AddParameter(cmd, "@" + parameters[i], values[i]);
                    }
                }

                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = entity.GetType();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
                return item;
            }
        }

        public T First(string parameter, object value)
        {
            using (DbConnection con = Context.getConnection())
            {
                StringBuilder builder = new StringBuilder(getSelectString());
                builder.Append(" where ");
                builder.Append(parameter);
                builder.Append(" = @");
                builder.Append(parameter);
                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                AddParameter(cmd, "@" + parameter, value);

                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = entity.GetType();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           entity,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
                return entity;
            }
        }

        public T First(string parameter, object value, T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                StringBuilder builder = new StringBuilder(getSelectString());
                builder.Append(" where ");
                builder.Append(parameter);
                builder.Append(" = @");
                builder.Append(parameter);
                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                AddParameter(cmd, "@" + parameter, value);

                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = entity.GetType();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
                return item;
            }
        }

        public T Where(object id)
        {
            return Where(id, entity);
        }

        public T Where(object id, T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getSelectByIDString(), CommandType.Text);
                AddParameter(cmd, "@" + IndexName, id);
                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = entity.GetType();

                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
            }
            return item;
        }

        public List<T> Select()
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getSelectString(), CommandType.Text);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string id)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getSelectByIDString(), CommandType.Text);
                AddParameter(cmd, "@" + IndexName, id);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string sql, bool suffix)
        {
            using (DbConnection con = Context.getConnection())
            {
                string select = !suffix ? getSelectString(sql) : getSelectString() + sql;

                DbCommand cmd = getCommand(con, select, CommandType.Text);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Query(string sql)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, sql, CommandType.Text);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string parameter, object value, string sql, bool suffix)
        {
            using (DbConnection con = Context.getConnection())
            {
                StringBuilder builder = new StringBuilder();
                if (!suffix)
                {
                    builder.Append(getSelectString(sql));
                }
                else
                {
                    builder.Append(" where ");
                    builder.Append(parameter);
                    builder.Append(" = @");
                    builder.Append(parameter);
                    builder.Append(sql);
                }

                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                AddParameter(cmd, "@" + parameter, value);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string parameter, object value)
        {
            using (DbConnection con = Context.getConnection())
            {
                StringBuilder builder = new StringBuilder(getSelectString());
                builder.Append(" where ");
                builder.Append(parameter);
                builder.Append(" = @");
                builder.Append(parameter);
                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                AddParameter(cmd, "@" + parameter, value);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string [] parameters, object [] values)
        {
            using (DbConnection con = Context.getConnection())
            {
                StringBuilder builder = new StringBuilder(getSelectString());
                builder.Append(" where ");
                for (Int32 i = 0; i < parameters.Length; i++)
                {
                    builder.Append(parameters[i]);
                    builder.Append(" = @");
                    builder.Append(parameters[i]);
                    if (i + 1 < parameters.Length)
                        builder.Append(" and ");
                }

                DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                if (parameters != null)
                {
                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        AddParameter(cmd, "@" + parameters[i], values[i]);
                    }
                }
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string where, string [] parameters, object [] values)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getSelectString(where), CommandType.Text);
                if (parameters != null)
                {
                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        AddParameter(cmd, "@" + parameters[i], values[i]);
                    }
                }
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Select(string where, string parameter, object value)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getSelectString(where), CommandType.Text);
                if (parameter != null)
                {
                    AddParameter(cmd, "@" + parameter, value);
                }
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Execute(string name)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, name, CommandType.StoredProcedure);
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public T First(string proc, string parameter, object value)
        {
            return First(proc, parameter, value, entity);
        }

        public T First(string proc, string parameter, object value, T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, proc, CommandType.StoredProcedure);
                AddParameter(cmd, "@" + parameter, value);
                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = item.GetType();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
                return item;
            }
        }

        public T First(string proc, string[] parameters, string[] values)
        {
            return First(proc, parameters, values, entity);
        }

        public T First(string proc, string [] parameters, string [] values, T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, proc, CommandType.StoredProcedure);
                if (parameters != null)
                {
                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        AddParameter(cmd, "@" + parameters[i], values[i]);
                    }
                }
                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    Type t = item.GetType();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                }
                else
                {
                    return default(T);
                }
                return item;
            }
        }


        public List<T> Execute(string name, string [] parameters, object [] values)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, name, CommandType.StoredProcedure);
                if (parameters != null)
                {
                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        AddParameter(cmd, "@" + parameters[i], values[i]);
                    }
                }
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public List<T> Execute(string name, string parameter, object value)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, name, CommandType.StoredProcedure);
                if (parameter != null)
                {
                    AddParameter(cmd, "@" + parameter, value);
                }
                DataSet ds = fill(cmd);
                Type t = entity.GetType();

                List<T> entities = new List<T>();
                IEnumerable<DataRow> rows = ds.Tables[0].AsEnumerable();
                foreach (DataRow row in rows)
                {
                    T item = new T();
                    for (int i = 0; i < Fields.Length; i++)
                    {
                        object[] fobj = new object[] { row[Fields[i]] };

                        if (fobj[0] != System.DBNull.Value)
                        {
                            t.InvokeMember(Fields[i],
                                           BindingFlags.SetProperty, null,
                                           item,
                                           fobj);
                        }
                    }
                    entities.Add(item);
                }
                return entities;
            }
        }

        public void ExecuteProc(string name, string[] parameters, object[] values)
        {
            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, name, CommandType.StoredProcedure);
                if (parameters != null)
                {
                    for (Int32 i = 0; i < parameters.Length; i++)
                    {
                        AddParameter(cmd, "@" + parameters[i], values[i]);
                    }
                }
                cmd.ExecuteNonQuery();
            }
        }

        public void Insert(T item)
        {
            entity = item;
            Insert();
        }

        public void Insert()
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    DbCommand cmd = getCommand(con, getInsertString(), CommandType.Text);
                    Type t = entity.GetType();

                    object value = null;
                    for (Int32 i = 0; i <= Fields.Length - 1; i++)
                    {
                        value = t.InvokeMember(Fields[i],
                                          BindingFlags.GetProperty, null,
                                          entity, new object[0]);
                        AddParameter(cmd, "@" + Fields[i], value);
                    }
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void Update(T item)
        {
            entity = item;
            Update();
        }

        public void Update()
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    DbCommand cmd = getCommand(con, getUpdateString(), CommandType.Text);
                    Type t = entity.GetType();

                    object value = null;
                    for (Int32 i = 0; i <= Fields.Length - 1; i++)
                    {
                            value = t.InvokeMember(Fields[i],
                                              BindingFlags.GetProperty, null,
                                              entity, new object[0]);
                            AddParameter(cmd, "@" + Fields[i], value);
                    }
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void Delete()
        {
            Delete(entity);
        }

        public void Delete(T item)
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    DbCommand cmd = getCommand(con, getDeleteString(), CommandType.Text);
                    Type t = item.GetType();

                    object value = null;
                    value = t.InvokeMember(IndexName,
                                      BindingFlags.GetProperty, null,
                                      item, new object[0]);
                    AddParameter(cmd, "@" + IndexName, value);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void Delete(string idx)
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    DbCommand cmd = getCommand(con, getDeleteString(), CommandType.Text);
                    AddParameter(cmd, "@" + IndexName, idx);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void Delete(string[] parameters, object[] values)
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    StringBuilder builder = new StringBuilder(getDeleteAllString());
                    if (parameters != null && parameters.Length>0)
                    {
                        builder.Append(" where ");

                        for (Int32 i = 0; i < parameters.Length; i++)
                        {
                            if (i > 0)
                            {
                                builder.Append(" and ");
                            }
                            builder.Append(parameters[i]);
                            builder.Append(" = @");
                            builder.Append(parameters[i]);
                        }
                    }

                    DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                    if (parameters != null && parameters.Length > 0)
                    {
                        for (Int32 i = 0; i < parameters.Length; i++)
                        {
                            AddParameter(cmd, "@" + parameters[i], values[i]);
                        }
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void Delete(string parameter, object value)
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    StringBuilder builder = new StringBuilder(getDeleteAllString());
                    if (parameter != null)
                    {
                        builder.Append(" where ");
                        builder.Append(parameter);
                        builder.Append(" = @");
                        builder.Append(parameter);
                    }

                    DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                    if (parameter != null)
                    {
                        AddParameter(cmd, "@" + parameter, value);
                    }

                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void DeleteAll()
        {
            using (DbConnection con = Context.getConnection())
            {
                try
                {
                    StringBuilder builder = new StringBuilder(getDeleteAllString());
                    DbCommand cmd = getCommand(con, builder.ToString(), CommandType.Text);
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public int GetCount(string index)
        {
            int count = 0;

            using (DbConnection con = Context.getConnection())
            {
                DbCommand cmd = getCommand(con, getCountByIDString(), CommandType.Text);
                AddParameter(cmd, "@" + IndexName, index);
                DataSet ds = fill(cmd);
                DataRow row = ds.Tables[0].AsEnumerable().FirstOrDefault();
                if (row != null)
                {
                    count = (int)row[0];
                }
            }
            return count;
        }
    }
}
