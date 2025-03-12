﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Collections.ObjectModel;
using VHMS;

namespace VHMS.DataAccess
{
    public class State
    {
        public static Collection<Entity.State> GetState()
        {
            string sException = string.Empty;
            Database db;
            DataSet dsList = null;
            Collection<Entity.State> objList = new Collection<Entity.State>();
                 Entity.State objState = new Entity.State();
            Entity.Country objCountry = new Entity.Country();

            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STATE);
                dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objState = new Entity.State();
                        objCountry = new Entity.Country();

                        objCountry.CountryID = Convert.ToInt32(drData["FK_CountryID"]);
                        objCountry.CountryName = Convert.ToString(drData["CountryName"]);
                        objState.Country = objCountry;
                        objState.StateID = Convert.ToInt32(drData["PK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objState.StateCode = Convert.ToString(drData["StateCode"]);
                        objState.IsActive = Convert.ToBoolean(drData["IsActive"]);
                        objList.Add(objState);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.State GetState | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objList;
        }
        public static Entity.State GetStateByID(int iStateID)
        {
            string sException = string.Empty;
            Database db;
            Entity.State objState = new Entity.State();
            Entity.Country objCountry = new Entity.Country();
            try
            {
                db = Entity.DBConnection.dbCon;
                DbCommand cmd = db.GetStoredProcCommand(constants.StoredProcedures.USP_SELECT_STATE);
                db.AddInParameter(cmd, "@PK_StateID", DbType.Int32, iStateID);
                DataSet dsList = db.ExecuteDataSet(cmd);

                if (dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow drData in dsList.Tables[0].Rows)
                    {
                        objState = new Entity.State();
                        objCountry = new Entity.Country();

                        objCountry.CountryID = Convert.ToInt32(drData["FK_CountryID"]);
                        objCountry.CountryName = Convert.ToString(drData["CountryName"]);
                        objState.Country = objCountry;
                        objState.StateID = Convert.ToInt32(drData["PK_StateID"]);
                        objState.StateName = Convert.ToString(drData["StateName"]);
                        objState.StateCode = Convert.ToString(drData["StateCode"]);
                        objState.IsActive = Convert.ToBoolean(drData["IsActive"]);
                    }
                }
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.State GetStateByID | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return objState;
        }

        public static int AddState(Entity.State objState)
        {
            string sException = string.Empty;
            int iID = 0;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_INSERT_STATE);
                oDb.AddOutParameter(cmd, "@PK_StateID", DbType.Int32, objState.StateID);
                oDb.AddInParameter(cmd, "@FK_CountryID", DbType.String, objState.Country.CountryID);
                oDb.AddInParameter(cmd, "@StateName", DbType.String, objState.StateName);
                oDb.AddInParameter(cmd, "@StateCode", DbType.String, objState.StateCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objState.IsActive);
                oDb.AddInParameter(cmd, "@FK_CreatedBy", DbType.Int32, objState.CreatedBy.UserID);
                iID = oDb.ExecuteNonQuery(cmd);
                if (iID != 0) iID = Convert.ToInt32(oDb.GetParameterValue(cmd, "@PK_StateID"));

            }
            catch (Exception ex)
            {
                sException = "State AddState | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return iID;
        }

        public static bool UpdateState(Entity.State objState)
        {
            string sException = string.Empty;
            int iID = 0; bool bResult = false;
            Database oDb;
            try
            {
                oDb = Entity.DBConnection.dbCon;
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_UPDATE_STATE);
                oDb.AddInParameter(cmd, "@PK_StateID", DbType.Int32, objState.StateID);
                oDb.AddInParameter(cmd, "@FK_CountryID", DbType.String, objState.Country.CountryID);
                oDb.AddInParameter(cmd, "@StateName", DbType.String, objState.StateName);
                oDb.AddInParameter(cmd, "@StateCode", DbType.String, objState.StateCode);
                oDb.AddInParameter(cmd, "@IsActive", DbType.Boolean, objState.IsActive);
                oDb.AddInParameter(cmd, "@FK_ModifiedBy", DbType.Int32, objState.ModifiedBy.UserID);
                if (iID != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.State UpdateState | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
        public static bool DeleteState(int ID, int UserID)
        {
            bool IsDeleted = false;
            Database oDb = Entity.DBConnection.dbCon;
            using (DbConnection oDbCon = oDb.CreateConnection())
            {
                oDbCon.Open();
                DbTransaction oTrans = oDbCon.BeginTransaction();
                try
                {
                    IsDeleted = DeleteState(oDb, ID, UserID, oTrans);
                    oTrans.Commit();

                    if (IsDeleted) Framework.InsertAuditLog("tState", "PK_StateID", ID.ToString(), (char)Entity.Common.DatabaseAction.DELETE, UserID);
                }
                catch (Exception ex)
                {
                    oTrans.Rollback();
                    throw ex;
                }
                finally
                {
                    oDbCon.Close();
                }
            }
            return IsDeleted;
        }
        private static bool DeleteState(Database oDb, int ID, int UserID, DbTransaction oTrans)
        {
            string sException = string.Empty;
            int iRemoveId = 0;
            bool bResult = false;
            try
            {
                DbCommand cmd = oDb.GetStoredProcCommand(constants.StoredProcedures.USP_DELETE_STATE);
                oDb.AddInParameter(cmd, "@PK_StateID", DbType.Int32, ID);
                iRemoveId = oDb.ExecuteNonQuery(cmd);
                if (iRemoveId != 0) bResult = true;
            }
            catch (Exception ex)
            {
                sException = "VHMS.DataAccess.State DeleteState | " + ex.ToString();
                Log.Write(sException);
                throw;
            }
            return bResult;
        }
    }
}
