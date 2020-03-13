﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using Alexander.Models.DAL;

namespace Alexander.Models
{
    public class Batch
    {
        private int batchID;
        private DateTime date;
        private int tank;
        private float wort_volume;
        private string beerType;
        private Recipe recipe_for_this_batch;

        public int BatchID { get => batchID; set => batchID = value; }
        public DateTime Date { get => date; set => date = value; }
        public int Tank { get => tank; set => tank = value; }
        public float Wort_volume { get => wort_volume; set => wort_volume = value; }
        public string BeerType { get => beerType; set => beerType = value; }
        public Recipe Recipe_for_this_batch { get => recipe_for_this_batch; set => recipe_for_this_batch = value; }

        public Batch() { }

        

        public List<Batch> get_Batches()
        {
            DBservices dbs = new DBservices();

            List<Batch> Batch_arr = dbs.get_BatchesDB();

            return Batch_arr;
        }

        public int insert()
        {
            DBservices dbs = new DBservices();
            int numEffected = dbs.insert(this);
            return numEffected;
        }


        public int Update()
        {
            int effected = 0;
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Batch_2020]");

            dbs.dt = edit(dbs.dt, this.batchID);

            effected = dbs.update(); // update DB

            return effected;
        }

        public DataTable edit(DataTable dt, int batchid)
        {
            DataRow dr = dt.Select("batch_id=" + batchid).First(); // gets the row where id == batchid

            dr["date"] = this.date;
            dr["beer_type"] = this.beerType;

            return dt;
        }

        public int delete_line(int row)
        {
            DBservices dbs = new DBservices();
            dbs = dbs.read("[Batch_2020]");

            try
            {
                dbs.dt.Select("batch_id=" + row).First().Delete(); // Delete a line in DataTable
                dbs.update(); // update the DB
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return 1;
        }
    }
}