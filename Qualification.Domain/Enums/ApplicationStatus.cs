using System.ComponentModel.DataAnnotations;

namespace Qualification.Domain.Enums;

public enum ApplicationStatus
{
    [Display(Name = "Yuborildi")]
    Yuborildi,

    [Display(Name = "Tekshirilmoqda")]
    Tekshirilmoqda,

    [Display(Name = "To'lov kutilmoqda")]
    TolovKutilmoqda,

    [Display(Name = "To'lov qilindi")]
    TolovQilindi,

    [Display(Name = "Test belgilandi")]
    TestBelgilandi,

    [Display(Name = "Test boshlandi")]
    TestBoshlandi,

    [Display(Name = "Bekor qilindi")]
    BekorQilindi,

    [Display(Name = "Yakunlandi")]
    Yakunlandi
}