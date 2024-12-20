﻿namespace LAKAPSAGAP.Services.Core.Helpers;

public class IdGenerator
{
    public static string PFX_USERINFO { get; set; } = "ACC_";
    public static string PFX_ATTACHMENT { get; set; } = "ATT_";
    public static string PFX_RELIEFRECEIVED { get; set; } = "BATCH_";
    public static string PFX_Warehouse { get; set; } = "WH_";
    public static string PFX_STOCKDETAIL { get; set; } = "STKDTL_";
    public static string PFX_STOCKITEM { get; set; } = "STKITM_";
    public static string PFX_CATEGORY { get; set; } = "CAT_";
    public static string PFX_UOM { get; set; } = "UOM_";
    public static string PFX_FLOOR { get; set; } = "FLR_";
    public static string PFX_RACK { get; set; } = "RCK_";
    public static string PFX_KIT { get; set; } = "KIT_";
    public static string PFX_KITCOMPONENT { get; set; } = "KITCT_";
    public static string PFX_PACKEDKIT { get; set; } = "KITPK_";
    public static string PFX_REQUEST { get; set; } = "RQST_";
    public static string PFX_REQUESTITEM { get; set; } = "RQSTITM_";
    public static string PFX_REQUESTATTACHMENT { get; set; } = "RQSTATT_";
    public static string PFX_RELIEFSENT { get; set; } = "SNT_";
    public static string PFX_RELIEFSENTITEM { get; set; } = "SNTITM_";
    public static string PFX_RELIEFSENTKIT { get; set; } = "SNTKIT_";
    public static string GenerateId(string prefix, int count)
    {
        try
        {
            var userCount = count + 1;
            return prefix + userCount.ToString("D3");
        }
        catch (Exception)
        {
            throw;
        }
    }
}
