﻿namespace SMS.Utility
{
    public static class SweetMessages
    {
        //public static string _SideBarCollapse = "sidebar-collapse";
        //public static string _SaveSuccess = "Save successful";
        //public static string _SaveError = "Save failed";
        //public static string _DeleteSuccess = "Delete successful";
        //public static string _DeleteError = "Delete failed";
        //public static string SaveSuccessOK(string _msg)
        //{
        //    return "Swal.fire('success','" + _msg + "','success')";
        //}
        //public static string SaveSuccessOK()
        //{
        //    return "Swal.fire('success','Saved successful','success')";
        //}
        //public static string DataExists()
        //{
        //    return "Swal.fire('warning','Data Already Exists','warning')";
        //}
        //public static string ProperInformrtion()
        //{
        //    return "Swal.fire('warning','Please enter proper information','warning')";
        //}

        //public static string SaveErrorOK(string _msg)
        //{
        //    return "Swal.fire('error','" + _msg + "','error')";
        //}
        //public static string SaveErrorOK()
        //{
        //    return "Swal.fire('error','Saved failed','error')";
        //}
        public static string SaveWarningOK(string _msg)
        {
            return "Swal.fire('warning','" + _msg + "','warning')";
        }
        //public static string SaveWarningOK()
        //{
        //    return "Swal.fire('warning','Data not found','warning')";
        //}
        public static string SaveSuccess(string _msg)
        {
            return @"Swal.fire({position: 'top-end',icon: 'success', title: '" + _msg + @"', showConfirmButton: false,timer: 1500})";
        }
        public static string SaveSuccess()
        {
            return @"Swal.fire({position: 'top-end',icon: 'success', title: 'Save successful', showConfirmButton: false,timer: 1500})";
        }
        //public static string ApprovalData()
        //{
        //    return "Swal.fire('warning','No data found for approval','warning')";
        //}

    }
}
