using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Net;
using WebTMAIntegration.Data.Helpers;
using WebTMAIntegration.Data.Interfaces;
using WebTMAIntegration.Models;
using WebTMAIntegration.Models.Entities;
using WebTMAIntegration.ViewModels;

namespace WebTMAIntegration.Data
{
    public class BuildingRepository : IBuildingRepository
    {
        private readonly SqlClientHelper _db;

        public BuildingRepository(SqlClientHelper db)
        {
            _db = db;
        }

        // SINGLE INSERT
        /* public async Task<int> SaveBuildingAsync(BuildingEntity building)
         {
             if (building == null)
             {
                 throw new ArgumentNullException(nameof(building));
             }

            SqlParameter[] parameters =
            {
                new SqlParameter("@CreatedBy", SqlDbType.Int)
                {
                    Value = building.CreatedBy
                },

                new SqlParameter("@BuildingTypeId", SqlDbType.Int)
                {
                    Value = building.BuildingTypeId
                },

                new SqlParameter("@BuildingName", SqlDbType.NVarChar, -1)
                {
                    Value = string.IsNullOrWhiteSpace(building.BuildingName)
                        ? (object)DBNull.Value
                        : building.BuildingName
                },

                new SqlParameter("@IsActive", SqlDbType.Bit)
                {
                    Value = building.IsActive
                },

                new SqlParameter("@SiteCode", SqlDbType.NVarChar, 100)
                {
                    Value = string.IsNullOrWhiteSpace(building.SiteCode)
                        ? (object)DBNull.Value
                        : building.SiteCode
                },

                new SqlParameter("@BuildingCode", SqlDbType.NVarChar, 50)
                {
                    Value = string.IsNullOrWhiteSpace(building.BuildingCode)
                        ? (object)DBNull.Value
                        : building.BuildingCode
                },

                new SqlParameter("@CityId", SqlDbType.Int)
                {
                    Value = building.CityId ?? (object)DBNull.Value
                },

                new SqlParameter("@Address", SqlDbType.NVarChar, 150)
                {
                    Value = string.IsNullOrWhiteSpace(building.Address)
                        ? (object)DBNull.Value
                        : building.Address
                },

                new SqlParameter("@ShiftIds", SqlDbType.NVarChar, -1)
                {
                    Value = string.IsNullOrWhiteSpace(building.ShiftIds)
                        ? (object)DBNull.Value
                        : building.ShiftIds
                },

                // Not available in entity
                new SqlParameter("@UserId", SqlDbType.NVarChar, -1)
                {
                    Value = DBNull.Value
                },

                new SqlParameter("@IsSyncRequired", SqlDbType.Bit)
                {
                    Value = building.IsSyncRequired ?? (object)DBNull.Value
                },

                // Not available in entity
                new SqlParameter("@IsExcluded", SqlDbType.Bit)
                {
                    Value = DBNull.Value
                },

                // Not available in entity
                new SqlParameter("@ExcludedReason", SqlDbType.NVarChar, 150)
                {
                    Value = DBNull.Value
                },

                new SqlParameter("@IsFireDrillApplicable", SqlDbType.Bit)
                {
                    Value = building.IsFireDrillApplicable ?? (object)DBNull.Value
                },

                new SqlParameter("@FireDrillBuildingStartDate", SqlDbType.DateTime)
                {
                    Value = building.FireDrillBuildingStartDate ?? (object)DBNull.Value
                },

                new SqlParameter("@newId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Output
                }
            };

             return await _db.ExecuteNonQueryAsync(
                 query: "Insert_Building",
                 commandType: CommandType.StoredProcedure,
                 parameters: parameters);
         }
        */
        // MULTIPLE INSERT
        public async Task<int> SaveBuildingsAsync(List<BuildingEntity> buildings)
        {
            if (buildings == null || !buildings.Any())
                return 0;

            int totalRowsAffected = 0;

            await using SqlConnection conn =
                await _db.GetOpenConnectionAsync();

            await using SqlTransaction transaction =
                (SqlTransaction)await conn.BeginTransactionAsync();

            try
            {
                foreach (var building in buildings)
                {
                    SqlParameter[] parameters =
                    {
                       

                        new SqlParameter("@BuildingId", SqlDbType.Int)
                        {
                            Value = building.BuildingId
                        },

                        new SqlParameter("@SiteId", SqlDbType.Int)
                        {
                            Value = building.SiteId
                        },

                        new SqlParameter("@SiteCode", SqlDbType.NVarChar, 100)
                        {
                            Value = string.IsNullOrWhiteSpace(building.SiteCode)
                                ? (object)DBNull.Value
                                : building.SiteCode
                        },

                         new SqlParameter("@CityId", SqlDbType.Int)
                        {
                            Value = building.CityId ?? (object)DBNull.Value
                        },

                        new SqlParameter("@BuildingName", SqlDbType.NVarChar, -1)
                        {
                            Value = string.IsNullOrWhiteSpace(building.BuildingName)
                                ? (object)DBNull.Value
                                : building.BuildingName
                        },

                         new SqlParameter("@BuildingTypeId", SqlDbType.Int)
                        {
                            Value = building.BuildingTypeId
                        },

                        new SqlParameter("@BuildingCode", SqlDbType.NVarChar, 10)
                        {
                            Value = string.IsNullOrWhiteSpace(building.BuildingCode)
                                ? (object)DBNull.Value
                                : building.BuildingCode
                        },

                        new SqlParameter("@Address", SqlDbType.NVarChar, 150)
                        {
                            Value = string.IsNullOrWhiteSpace(building.Address)
                                ? (object)DBNull.Value
                                : building.Address
                        },

                        new SqlParameter("@IsActive", SqlDbType.Bit)
                        {
                            Value = building.IsActive
                        },

                        new SqlParameter("@RegionId", SqlDbType.Int)
                        {
                            Value = building.RegionId ?? 1
                        },

                        new SqlParameter("@CreatedBy", SqlDbType.Int)
                        {
                            Value = building.CreatedBy ?? 1
                        },
                    };

                    int rows = await _db.ExecuteNonQueryAsync(
                        query: "usp_InsertBuilding",
                        commandType: CommandType.StoredProcedure,
                        parameters: parameters,
                        connection: conn,
                        transaction: transaction
                    );


                    totalRowsAffected += rows;
                }

                await transaction.CommitAsync();

                return totalRowsAffected;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        // GET ALL
       public async Task<List<BuildingViewModel>> GetBuildingsAsync()
        {
            DataTable dt =
                await _db.ExecuteQueryAsync(
                    "usp_GetBuildings",
                    CommandType.StoredProcedure
                );

            List<BuildingViewModel> buildings =
                new List<BuildingViewModel>();

            if (dt.Rows.Count == 0)
            {
                return buildings;
            }

            foreach (DataRow row in dt.Rows)
            {
                buildings.Add(new BuildingViewModel
                {
                    BuildingId = row["BuildingId"] != DBNull.Value
                        ? Convert.ToInt32(row["BuildingId"]) : 0,

                    SiteName = row["SiteName"]?.ToString(),

                    SiteCodeValue = row["SiteCodeValue"]?.ToString(),

                    BuildingName = row["BuildingName"]?.ToString(),

                    BuildingCode = row["BuildingCode"]?.ToString(),

                    BuildingTypeId = row["BuildingTypeId"] != DBNull.Value
                        ? Convert.ToInt32(row["BuildingTypeId"]) : 0,

                    Address = row["Address"]?.ToString(),

                    IsActive = row["IsActive"] != DBNull.Value && Convert.ToBoolean(row["IsActive"]),

                    CreatedBy = row["CreatedBy"] != DBNull.Value
                        ? Convert.ToInt32(row["CreatedBy"]) : 0,

                    CreateDate = (DateTime)row["CreateDate"]
                });
            }

            return buildings;
        }
    }
}