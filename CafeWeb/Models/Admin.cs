//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CafeWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Admin
    {
        public int id { get; set; }

        [Required(ErrorMessage ="Kullan�c� ad� zorunludur")]
        public string kullaniciAdi { get; set; }

        [Required(ErrorMessage = "�ifrenizi giriniz")]
        public string Sifre { get; set; }
        public Nullable<byte> role { get; set; }

        public  virtual bool BeniHatirla { get; set; }
    }
}
