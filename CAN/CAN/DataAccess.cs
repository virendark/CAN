using CAN.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using CAN.ViewModels;
namespace CAN
{
    public class DataAccess
    {
        SQLiteConnection dbConn;
        public DataAccess()
        {
            dbConn = DependencyService.Get<ISQLite>().GetConnection();

            dbConn.CreateTable<FamilyRegister>();
            dbConn.CreateTable<ChildRegister>();
            dbConn.CreateTable<UserToekn>();
            dbConn.CreateTable<UserPinTable>();
            dbConn.CreateTable<GrowthRegister>();
            dbConn.CreateTable<DataMonths>();
            dbConn.CreateTable<User>();
            dbConn.CreateTable<ColumnType>();
            dbConn.CreateTable<ColumnValue>();
            dbConn.CreateTable<Location>();
            dbConn.CreateTable<LMSBMI>();
            dbConn.CreateTable<LMSHAZ>();
            dbConn.CreateTable<LMSWAZ>();
            dbConn.CreateTable<LMSWFH>();
            dbConn.CreateTable<LMSWFL>();
            dbConn.CreateTable<RedFlagRegister>();
            dbConn.CreateTable<TblGrowthRegisterMother>();
            dbConn.CreateTable<TblPushDataTime>();
            dbConn.CreateTable<MonthlyMonitoring>();
        }
        //public List<RedFlagDetails> GetRedFlagChildData(string ChildId, bool RedFlag, int DataMonthId)
        //{
        //    return dbConn.Query<RedFlagDetails>("select GrowthRegister.GenderID, GrowthRegister.GrowthId,GrowthRegister.ChildId, GrowthRegister.AnyRedFlag,RedFlagRegister.DateMonthId,RedFlagRegister.RedFlagId from [GrowthRegister] left outer join [RedFlagRegister] on GrowthRegister.ChildId=RedFlagRegister.ChildId  and RedFlagRegister.DateMonthId='" + DataMonthId + "' where GrowthRegister.DataMonthId='" + DataMonthId + "'  and GrowthRegister.ChildId='" + ChildId + "'");
        //}
        //public List<GrowthRegister> GetListOfRedFlag(string ChildId,int DataMonthId)
        //{
        //    return dbConn.Query<GrowthRegister>("select * from [GrowthRegister]  where GrowthRegister.DataMonthId='" + DataMonthId + "'  and GrowthRegister.ChildId='" + ChildId + "'");
        //}
        public List<ChildMonthlyData> GetListOfRedFlag(long LocationID,  int DataMonthId)
        {
            return dbConn.Query<ChildMonthlyData>("SELECT  ChildRegister.ChildName, ChildRegister.DOB, ChildRegister.ChildStatus, ChildRegister.GenderID,ColumnValue.ColumnValue as GenderName, ChildRegister.ChildCode, FamilyRegister.FamilyCode,GrowthRegister.GrowthId,GrowthRegister.AnyRedFlag, ChildRegister.ChildId, GrowthRegister.DataMonthId FROM [ColumnValue] INNER JOIN  [ChildRegister] on ChildRegister.GenderId=ColumnValue.ColumnValueId INNER JOIN [FamilyRegister] on ChildRegister.FamilyId = FamilyRegister.FamilyId  LEFT OUTER JOIN [GrowthRegister] on ChildRegister.ChildId = GrowthRegister.ChildId and GrowthRegister.DataMonthId = " + DataMonthId + " WHERE FamilyRegister.LocationId = " + LocationID);
        }
        public List<RedFlagDetails> GetRedFlagChildData(string ChildId, bool RedFlag, int DataMonthId)
        {
            return dbConn.Query<RedFlagDetails>("select GrowthRegister.GenderID, GrowthRegister.GrowthId,GrowthRegister.ChildId, GrowthRegister.AnyRedFlag,RedFlagRegister.DateMonthId,RedFlagRegister.RedFlagId from [GrowthRegister] left outer join [RedFlagRegister] on GrowthRegister.ChildId=RedFlagRegister.ChildId  and RedFlagRegister.DateMonthId='" + DataMonthId + "' where GrowthRegister.DataMonthId='" + DataMonthId + "'  and GrowthRegister.ChildId='" + ChildId + "'");
        }
        //public List<ChildMonthlyData> GetChildMonthlyData(string FamilyId,  int StatusId,int DataMonthId)
        //{
        //    return dbConn.Query<ChildMonthlyData>("select  ChildRegister.ChildCode,ChildRegister.ChildId,ChildRegister.GenderID,ChildRegister.ChildName,ChildRegister.ChildStatus,ChildRegister.DOB,GrowthRegister.GrowthId,GrowthRegister.DataMonthId,  GrowthRegister.AnyRedFlag from [ChildRegister] left outer join [GrowthRegister] on ChildRegister.ChildId=GrowthRegister.ChildId  and GrowthRegister.DataMonthId='" + DataMonthId + "' where ChildRegister.FamilyId='" + FamilyId + "'  and ChildRegister.ChildStatus='" + StatusId + "'");
        //}
        public List<ChildMonthlyData> GetChildMonthlyData(long LocationID, long StatusId, int DataMonthId)
        {
            return dbConn.Query<ChildMonthlyData>("SELECT  ChildRegister.ChildName, ChildRegister.DOB, ChildRegister.ChildStatus, ChildRegister.GenderID,ColumnValue.ColumnValue as GenderName, ChildRegister.ChildCode, FamilyRegister.FamilyCode,GrowthRegister.GrowthId,GrowthRegister.AnyRedFlag, ChildRegister.ChildId, GrowthRegister.DataMonthId FROM [ColumnValue] INNER JOIN  [ChildRegister] on ChildRegister.GenderId=ColumnValue.ColumnValueId INNER JOIN [FamilyRegister] on ChildRegister.FamilyId = FamilyRegister.FamilyId  LEFT OUTER JOIN [GrowthRegister] on ChildRegister.ChildId = GrowthRegister.ChildId and GrowthRegister.DataMonthId = " + DataMonthId + " WHERE FamilyRegister.LocationId = " + LocationID + " and ChildRegister.ChildStatus=" + StatusId );
        }
        public List<MotherWithChildDetails> GetMotherWithChildDetails(long LocationId,int DataId,int StatusId)
        {
            return dbConn.Query<MotherWithChildDetails>("select FamilyRegister.FamilyCode,FamilyRegister.FamilyId,FamilyRegister.FamilyType,FamilyRegister.FatherName,FamilyRegister.MotherName,FamilyRegister.LocationId ,FamilyRegister.status,TblGrowthRegisterMother.GrowthId,TblGrowthRegisterMother.DataMonthId,TblGrowthRegisterMother.HighRiskMotherHistory,TblGrowthRegisterMother.ChildExpected  from [FamilyRegister] left outer join [TblGrowthRegisterMother] on FamilyRegister.FamilyId=TblGrowthRegisterMother.FamilyId  and TblGrowthRegisterMother.DataMonthId=" + DataId + " where FamilyRegister.LocationId=" + LocationId+ "  and FamilyRegister.status=" + StatusId + "");
        }
        public List<MotherWithChildDetails> GetMotherWithChildDetailsWithOutDataId(long LocationId, int StatusId)
        {
            return dbConn.Query<MotherWithChildDetails>("select * from [FamilyRegister] left outer join [TblGrowthRegisterMother] on FamilyRegister.FamilyId=TblGrowthRegisterMother.FamilyId where FamilyRegister.LocationId='" + LocationId + "' and FamilyRegister.status='" + StatusId + "'");
        }
        
        public List<TblPushDataTime> Getpushdatatime()
        {
            return dbConn.Query<TblPushDataTime>("select * from [TblPushDataTime]");
        }
        public int SavepushDataTime(TblPushDataTime tblPushDataTime)
        {
            return dbConn.Insert(tblPushDataTime);
        }
        public List<TblPushDataTime> deletepushdatatime()
        {
            return dbConn.Query<TblPushDataTime>("delete  from [TblPushDataTime]");
        }
        public List<TblGrowthRegisterMother> FindGrowthRegisterMotherSingleData(string GId)
        {
            return dbConn.Query<TblGrowthRegisterMother>("Select DISTINCT  * From [TblGrowthRegisterMother] where GrowthId='" + GId + "'");
        }
        public List<TblGrowthRegisterMother> GetAllGrowthRegisterMotherEntry(int DataId)
        {
            return dbConn.Query<TblGrowthRegisterMother>("Select DISTINCT  * From [TblGrowthRegisterMother] where DataMonthId='"+DataId+"'");
        }
        public List<TblGrowthRegisterMother> GetAllGrowthRegisterMotherDateTime(DateTime dateTime)
        {
            return dbConn.Query<TblGrowthRegisterMother>("Select DISTINCT  * From [TblGrowthRegisterMother] where DOU>='" + dateTime+"'");
        }
        public List<TblGrowthRegisterMother> GetAllGrowthRegisterMother()
        {
            return dbConn.Query<TblGrowthRegisterMother>("Select DISTINCT  * From [TblGrowthRegisterMother]");
        }
        public List<TblGrowthRegisterMother> deleteMother()
        {
            return dbConn.Query<TblGrowthRegisterMother>("delete   From [TblGrowthRegisterMother] ");
        }
        public int SaveGrowthRegisterMother(TblGrowthRegisterMother GrowthRegisterMother)
        {
            return dbConn.Insert(GrowthRegisterMother);
        }
        public List<TblGrowthRegisterMother> deleteGrowthRegisterMother(string FID, string GID)
        {
            return dbConn.Query<TblGrowthRegisterMother>("delete   From [TblGrowthRegisterMother] where FamilyId='" + FID + "' and GrowthId='" + GID + "'");
        }
        public List<TblGrowthRegisterMother> FindGrowthRegisterMother(string CID)
        {
            return dbConn.Query<TblGrowthRegisterMother>("Select   * From [TblGrowthRegisterMother] where FamilyId='" + CID + "'");
        }
       
        public List<FamilyRegister> GetAllFamily()
        {
            return dbConn.Query<FamilyRegister>("Select DISTINCT  * From [FamilyRegister]" );
        }
        public List<FamilyRegister> GetAllFamilydateTime(DateTime dateTime)
        {
            return dbConn.Query<FamilyRegister>("Select DISTINCT  * From [FamilyRegister] where DOU>='" + dateTime+"'");
        }
        public List<FamilyRegister> GetFamilyRegisterDateByData(DateTime dateTime)
        {
            return dbConn.Query<FamilyRegister>("Select DISTINCT  * From [FamilyRegister] where RegisterDate>'" + dateTime + "'");
        }
        public List<FamilyRegister> DeleteFamily()
        {
            return dbConn.Query<FamilyRegister>("delete   From [FamilyRegister]");
        }
        public List<ChildRegister> DeletChildById(string guid)
        {
            return dbConn.Query<ChildRegister>("delete from [ChildRegister]  where ChildId='" + guid + "'");
        }
        public List<FamilyRegister> DeleteFamilyById(Guid guid)
        {
            return dbConn.Query<FamilyRegister>("delete from [FamilyRegister]  where FamilyId='" + guid+"'");
        }
        public List<FamilyRegister> GetAllFamilyByLocation(long LocationId)
        {
            return dbConn.Query<FamilyRegister>("Select * From [FamilyRegister] where LocationId=" + LocationId);
        }
        public List<FamilyRegister> FindFamilyId(Guid FID)
        {
            return dbConn.Query<FamilyRegister>("Select   * From [FamilyRegister] where FamilyId='"+FID+"'");
        }
        public int SaveFamilyData(FamilyRegister familyRegister)
        {
            return dbConn.Insert(familyRegister);
        }
        public List<ChildRegister> FindChildId(string CID)
        {
            return dbConn.Query<ChildRegister>("Select   * From [ChildRegister] where FamilyId='" + CID + "'");
        }
        public List<ChildRegister> FindChildByData(string CID)
        {
            return dbConn.Query<ChildRegister>("Select DISTINCT  * From [ChildRegister] where ChildId='" + CID + "'");
        }
        public List<ChildRegister> GetAllChildDatetime(DateTime dateTime)
        {
            return dbConn.Query<ChildRegister>("Select DISTINCT  * From [ChildRegister] where DOU >'" + dateTime +"'");
        }
        public List<ChildRegister> GetAllChild()
        {
            return dbConn.Query<ChildRegister>("Select DISTINCT  * From [ChildRegister]");
        }
        public List<ChildRegister> GetChildRegisterDateByData( DateTime dateTime,string FID)
        {
            return dbConn.Query<ChildRegister>("Select DISTINCT  * From [ChildRegister] where FamilyId='"+FID+"' and  RegisterDate>'" + dateTime+"'");
        }
        public List<ChildRegister> DeleteChild()
        {
            return dbConn.Query<ChildRegister>("delete From [ChildRegister]");
        }
        public List<ChildRegister> GetMonthDataList( long id)
        {
            return dbConn.Query<ChildRegister>("Select DISTINCT  * From [ChildRegister] as c inner join [FamilyRegister] as f on c.FamilyId == f.FamilyId where f.LocationId='" + id+"'");
        }
        public int SaveChildData(ChildRegister childRegister)
        {
            return dbConn.Insert(childRegister);
        }
        public List<UserToekn> GetUserToken()
        {
            return dbConn.Query<UserToekn>("select * from [UserToekn]");
        }
        public int SaveUserToken(UserToekn userToekn)
        {
            return dbConn.Insert(userToekn);
        }
        public List<UserPinTable> GetUserPin()
        {
            return dbConn.Query<UserPinTable>("select * from UserPinTable");
        }
        public List<UserPinTable> DeleteUserPin()
        {
            return dbConn.Query<UserPinTable>("delete  from UserPinTable");
        }
        public int SetPin(UserPinTable userPinTable)
        {
            return dbConn.Insert(userPinTable);
        }
        public List<GrowthRegister> GetGrowthRegistersDateTime(DateTime dateTime)
        {
            return dbConn.Query<GrowthRegister>("select * from GrowthRegister where Dou>='" + dateTime+"'");
        }
        public List<GrowthRegister> GetGrowthRegisters()
        {
            return dbConn.Query<GrowthRegister>("select * from GrowthRegister");
        }
        public List<GrowthRegister> DeleteGrowthRegisters()
        {
            return dbConn.Query<GrowthRegister>("delete  from GrowthRegister");
        }
        public List<GrowthRegister> DeleteGrowthRegisterById(string GId)
        {
            return dbConn.Query<GrowthRegister>("delete  from GrowthRegister where GrowthId='"+GId+"'");
        }
        public List<GrowthRegister> GetGrowthRegisterById(string GId)
        {
            return dbConn.Query<GrowthRegister>("select *  from GrowthRegister where GrowthId='" + GId + "'");
        }
        public List<GrowthRegister> GetGrowthChildById(string CId)
        {
            return dbConn.Query<GrowthRegister>("select *  from GrowthRegister where ChildId='" + CId + "'");
        }
        public int SaveGrowthRegister(GrowthRegister growthRegister)
        {
            return dbConn.Insert(growthRegister);
        }
        public List<DataMonths> GetDataMonths()
        {
            return dbConn.Query<DataMonths>("select * from DataMonths");
        }
        public List<DataMonths> GetDataMonthByID(int Datamonthid)
        {
            return dbConn.Query<DataMonths>("select * from DataMonths where Datamonthid='"+ Datamonthid + "'");
        }
        
        public List<DataMonths> GetDataMonthsFormate()
        {
            return dbConn.Query<DataMonths>("select * from DataMonths");
        }
        public int saveDataMonths(DataMonths dataMonths)
        {
            return dbConn.Insert(dataMonths);
        }
        public List<DataMonths> deleteDataMonths()
        {
            return dbConn.Query<DataMonths>("delete  from DataMonths");
        }
        public List<User> GetUsers()
        {
            return dbConn.Query<User>("select * from User");
        }
        public List<User> DeleteUser()
        {
            return dbConn.Query<User>("delete from User");
        }
        public int saveDataUser(User user)
        {
            return dbConn.Insert(user);
        }
        public List<User> updateDataUser(string Token)
        {
            return dbConn.Query<User>("update User set Token='"+ Token + "'");
        }
        public List<ColumnType> GetColumnTypes()
        {
            return dbConn.Query<ColumnType>("select * from ColumnType");
        }
        public int saveColumnType(ColumnType columnType)
        {
            return dbConn.Insert(columnType);
        }
        public List<ColumnType> DeleteColumnType()
        {
            return dbConn.Query<ColumnType>("delete from ColumnType");
        }
        public List<ColumnValue> GetColumnValues()
        {
            return dbConn.Query<ColumnValue>("select * from ColumnValue");
        }
        public List<ColumnValue> GetColumnValuesBytext( int columnTypeId)
        {
            return dbConn.Query<ColumnValue>("select * from ColumnValue where columnTypeId="+columnTypeId);
        }
        public List<ColumnValue> DeleteColumnValues()
        {
            return dbConn.Query<ColumnValue>("delete from ColumnValue");
        }
        public int saveColumnValue(ColumnValue columnValue)
        {
            return dbConn.Insert(columnValue);
        }
        public List<Location> GetVillageLocations( int locationTypeId)
        {
            return dbConn.Query<Location>("select * from Location where locationTypeId="+locationTypeId);
        }
        public int saveLocation(Location location)
        {
            return dbConn.Insert(location);
        }
        public List<Location> DeleteLocation()
        {
            return dbConn.Query<Location>("delete from Location");
        }
        public List<Location> GetLocation()
        {
            return dbConn.Query<Location>("select * from Location");
        }
        public int saveLMSBMI(LMSBMI lMSBMI)
        {
            return dbConn.Insert(lMSBMI);
        }
        public List<LMSBMI> GetLMSBMI()
        {
            return dbConn.Query<LMSBMI>("select * from LMSBMI");
        }
        public List<LMSBMI> DeleteLMSBMI()
        {
            return dbConn.Query<LMSBMI>("delete  from LMSBMI");
        }
        public List<LMSHAZ> GetLMSHAZ(int Gender,int Ageindays)
        {

            return dbConn.Query<LMSHAZ>("select * from LMSHAZ where GenderID='"+Gender+"'and AgeInDays='"+Ageindays+"'");
        }
        public List<LMSHAZ> GetLMSHAZ()
        {
            return dbConn.Query<LMSHAZ>("select * from LMSHAZ");
        }
        public List<LMSHAZ> deleteLMSHAZ()
        {
            return dbConn.Query<LMSHAZ>("delete from LMSHAZ");
        }
        public int saveLMSHAZ(LMSHAZ mSHAZ)
        {
            return dbConn.Insert(mSHAZ);
        }
        public int saveLMSWAZ(LMSWAZ lMSWAZ)
        {
            return dbConn.Insert(lMSWAZ);
        }
        public List<LMSWAZ> GetLMSWAZ()
        {
            return dbConn.Query<LMSWAZ>("select * from LMSWAZ");
        }
        public List<LMSWAZ> deleteLMSWAZ()
        {
            return dbConn.Query<LMSWAZ>("delete  from LMSWAZ");
        }
        public List<LMSWFH> GetLMSWFH()
        {
            return dbConn.Query<LMSWFH>("select * from LMSWFH");
        }
        public List<LMSWFH> deleteLMSWFH()
        {
            return dbConn.Query<LMSWFH>("delete  from LMSWFH");
        }
        public int saveLMSWFH(LMSWFH lMSWFH)
        {
            return dbConn.Insert(lMSWFH);
        }
        public int saveLMSWFL(LMSWFL lMSWFf)
        {
            return dbConn.Insert(lMSWFf);
        }
        public List<LMSWFL> GetLMSWFL()
        {
            return dbConn.Query<LMSWFL>("select * from LMSWFL");
        }
        public List<LMSWFL> deleteLMSWFL()
        {
            return dbConn.Query<LMSWFL>("delete  from LMSWFL");
        }
        public List<ChildRegister> FindSingleChildDetails(string CID)
        {
            return dbConn.Query<ChildRegister>("Select   * From [ChildRegister] where ChildId='" + CID + "'");
        }
        public List<LMSWAZ> GetLMSWAZ(int Gender, int Ageindays)
        {

            return dbConn.Query<LMSWAZ>("select * from LMSWAZ where GenderID='"+Gender+"' and AgeInDays='"+Ageindays+"'");
        }
        public List<LMSWFL> GetLMSWFL(int Gender, double lengthInCM)
        {

            return dbConn.Query<LMSWFL>("select * from LMSWFL where GenderID='"+Gender+"' and LengthInCM='"+lengthInCM+ "'");
        }
        public List<LMSWFH> GetLMSWFH(int Gender, double HeightInCM)
        {
            return dbConn.Query<LMSWFH>("select * from LMSWFH where GenderID='" + Gender + "' and HeightInCM='" + HeightInCM + "'"); 
        }
        public List<LMSWFL> GetallLMSWFL()
        {
            return dbConn.Query<LMSWFL>("select * from LMSWFL");
        }
        public List<LMSBMI> GetLMSBMI(int Gender, double Ageindays)
        {

            return dbConn.Query<LMSBMI>("select * from LMSBMI where GenderID='"+Gender+"' and AgeInDays='"+Ageindays+"'");
        }
        public List<RedFlagRegister> GetAllRedFlagRegisterDateTime(DateTime dateTime)
        {
            return dbConn.Query<RedFlagRegister>("select * from RedFlagRegister where Dou>='" + dateTime+"'");
        }
        public List<RedFlagRegister> GetAllRedFlagRegister()
        {
            return dbConn.Query<RedFlagRegister>("select * from RedFlagRegister");
        }
        public List<RedFlagRegister> GetAllRedFlagRegisterById( string ChildId)
        {
            return dbConn.Query<RedFlagRegister>("select * from RedFlagRegister where ChildId='"+ChildId+"'");
        }
        public int SaveRedFlagRegister(RedFlagRegister redFlagRegister)
        {
            return dbConn.Insert(redFlagRegister);
        }
        public List<RedFlagRegister> DeleteRedFlagRegisterRegister()
        {
            return dbConn.Query<RedFlagRegister>("delete  from RedFlagRegister");
        }
        public List<RedFlagRegister> DeleteRedFlagRegisterRegisterById(string GId)
        {
            return dbConn.Query<RedFlagRegister>("delete  from RedFlagRegister where RedFlagId='" + GId + "'");
        }
        public List<RedFlagRegister> GetRedFlagRegisterRegisterById(string GId)
        {
            return dbConn.Query<RedFlagRegister>("select *  from RedFlagRegister where RedFlagId='" + GId + "'");
        }
        //public List<ListOfChildInRedFlagDetails> GetListOfChildInRedFlagData(string id,int DataMonthId)
        //{
        // return dbConn.Query<ListOfChildInRedFlagDetails>("Select DISTINCT  * From [ChildRegister]  inner join [RedFlagRegister]  on ChildRegister.ChildId == RedFlagRegister.ChildId where RedFlagRegister.ChildId='" + id + "' and RedFlagRegister.DateMonthId='" + DataMonthId + "' ");
        //}
        public List<RedFlagRegister> GetListOfChildInRedFlagData(string id, int DataMonthId)
        {
            return dbConn.Query<RedFlagRegister>("Select   * From  [RedFlagRegister]   where ChildId='" + id + "' and DateMonthId='" + DataMonthId + "' ");
        }
        public List<RedFlagRegister> GetRedFlagRegistercheckFlowup(string GId, int DataMonthId)
        {
            return dbConn.Query<RedFlagRegister>("select *  from RedFlagRegister where ChildId='" + GId + "' and DateMonthId='"+DataMonthId+"'");
        }
        public List<MonthlyMonitoring> GetMonthlyMonitoring()
        {
            return dbConn.Query<MonthlyMonitoring>("select * from MonthlyMonitoring");
        }
        public List<MonthlyMonitoring> GetMonthlyMonitoringById(int dataMonthId)
        {
            return dbConn.Query<MonthlyMonitoring>("select * from MonthlyMonitoring where DataMonthId=" + dataMonthId);
        }
        public List<MonthlyMonitoring> GetMonthlyMonitoringBySingleData(Guid GuidId)
        {
            return dbConn.Query<MonthlyMonitoring>("select * from MonthlyMonitoring where MonthlyMonitorId=" + GuidId);
        }
        public int saveMonthlyMonitoring(MonthlyMonitoring monthlyMonitoring)
        {
            return dbConn.Insert(monthlyMonitoring);
        }
        public List<MonthlyMonitoring> DeleteMonthlyMonitoring()
        {
            return dbConn.Query<MonthlyMonitoring>("delete from MonthlyMonitoring");
        }
        public List<MonthlyMonitoring> DeleteMonthlyMonitoringByID(string ID)
        {
            return dbConn.Query<MonthlyMonitoring>("delete from MonthlyMonitoring where MonthlyMonitorId="+ID);
        }
    }
}
