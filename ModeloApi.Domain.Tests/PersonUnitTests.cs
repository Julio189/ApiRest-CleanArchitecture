
using FluentAssertions;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Validations;

namespace ModeloApi.Domain.Tests;
public class PersonUnitTests
{
    [Fact(DisplayName = "Create Person with valid state")]
    public void CreatePerson_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Person(1, "Julio", "47205699860", "11975701286");
        action.Should().NotThrow<DomainValidationException>();
    }

    [Fact(DisplayName = "Create Person with negative Id")]
    public void CreatePerson_WithNegativeId_DomainExceptionInvalidId()
    {
        Action action = () => new Person(-1, "Julio", "47205699860", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Id!");
    }

    [Fact(DisplayName = "Create Person without name")]
    public void CreatePerson_WithoutNameValue_DomainExceptionNameRequired()
    {
        Action action = () => new Person(1, "", "47205699860", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Name is required!");
    }

    [Fact(DisplayName = "Create Person with null name")]
    public void CreatePerson_WithNullName_DomainExceptionNameRequired()
    {
        Action action = () => new Person(1, null, "47205699860", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Name is required!");
    }

    [Fact(DisplayName = "Create Person with short name")]
    public void CreatePerson_WithShortName_DomainExceptionShortName()
    {
        Action action = () => new Person(1, "Ju", "47205699860", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Name! Minimum 3 characteres!");
    }

    [Fact(DisplayName = "Create Person with long name")]
    public void CreatePerson_WithLongName_DomainExceptionLongName()
    {
        Action action = () => new Person(1, "jimRQojLCVsKWErNTtOk\r\npjPUsQvDrChBSSUqNAmk\r\nTTpMEMlBzwqXKNfSTJie\r\nUunQNBFiZCOXRNdAxbLv\r\naRBYDBgbIjauzkJOZSjG\r\nxSVJYjhmiBiFHEJYAkNE\r\nMUmVIuCdKRjqREJNWTmM\r\nXmbALeNMPHMZJMoeqQUY\r\nWHQrCuUdirZHKNEaLDgn\r\nXmsFeVltoyOOVPRhLdNG\r\nJZmXNiVHfwHQRIbDoWPh\r\nzqzWKxIxRZxcJWmzmRIl\r\nSSkOednsvVhUVCwsZfmd\r\nYYDlbKKrRLNZvRvCqQnb\r\nBVpfyXUJEKILFgXkUHCg\r\nsFealupAQMQKqXCyALEf\r\npoJxgAfbCoUfVTRYAZUM\r\nZAWviGpXHtkKtBosVfQr\r\nBKOqwhypHygeiizoAjMO\r\ntxeeRxjKKWuFPsmrLnWN\r\nicZhTGLofzPdOtdBsUmM\r\ncFawIhjHfiJvIoiKscpl\r\nlTWoXrnQzvWSUlxHeUlh\r\nHaGGRrsOBFKlvLcFOxti\r\njPxShQGdLNHQDFqgbebh\r\nnWMAdTvLUlyrLpsaeaMY\r\nWrgBGBAGiUYbXyZChCFQ\r\nooUpAsncfVdKhumxbGNW\r\nslRIRkTVCsBdigHbquku\r\nnRqfZEggaQjAUoXQjyXc\r\nQjdHbULKiGaYreHIzULO\r\nhVmwadMVQqWJZFvHHLKE\r\nsRfvAZzMuTeUFeSYLwQK\r\nEExJCekuhvoTEHnqfvvn\r\nnkTeiRqxbYPiCJFjwgVc\r\nqXmcKgaPfeelQPpVbcFP\r\nQknGQWeoSenfKTIQuGvJ\r\nisZZxSamdceDzaiGDDNZ\r\nksnYIthafOaMmjFcBwXu\r\nPAkRSwhVATGATLWbvZbG\r\nzwmQlCakDOFODbDcdWFE\r\nBvzbmzrwVmiAGZEVsHQC\r\nhdOycjHPGFxXgHxrbUJL\r\nLZYVIUFmDYniQHdvaAFu\r\nDxIgUNtRomKQpBnJgeKz\r\njcFbnMRDbpWMfSROBSLl\r\nxBIdcngXPVRTCKTnaVuU\r\nxfWSdvKgtdlOgYDxKnMi\r\nPCcqfZZVnVontZLlFvzq\r\nzKHtGRzGBWGIOiJfsvyU\r\nXqTcVbWaggnaTrLpyWQZ\r\nIBhoIbSDvMfMUFilHpmf\r\nOiAvNOXIBNzBUOpXNtux\r\nUGWKKoPlOiXbMYLMHJbL\r\nIKbqaCSDOhnSWVellSYH\r\nNcJyKItkboWLBdgWhOBJ\r\nsUTSuefKIDdVgcnIeYGx\r\nsPZRZTrdibXutgrUGguk\r\nQTETAdemyftJJRXcqepl\r\nUhkXDAbAgtuIQHIZPuNC\r\nsbiDpPhfGubAIOvJrtIq\r\npSZCvMSUUdyxLuGyonKy\r\nDgcjnkTmVVdYZXdKgXqd\r\nIzzvHLyrvNaHRnKEdQXS\r\njAhmsTKXbjcFjTuHmmCk\r\nkeKVeUNGMVToeTNzxMCC\r\nYJQlPYDoKXOgdQQeqMYd\r\nekaFjHIzcvzpjyensfJB\r\nwgmKIrJdwyHmcvgzekKZ\r\nNpAOIEhJGTktaIgWvJBQ\r\niMnZXbAEtCXnpQYZTrRO\r\nuBJlWxkhtcqNIAxYPoMp\r\nqnnjUnGozKPfWYhPpuOO\r\niRqurGvfuhmXZSNzBglH\r\nerouaXlMdAMBMmKakzcM\r\nKvfTGMKOcEOPQzIgrMUL\r\npijPSkzEUzNSigRAxPro\r\nuqDUaGCZQJRqXhVvidjd\r\nLYpGbmPAVCNaLCcTfEoM\r\nHTUYRoXGTjaSeYGQxszP\r\nndgcfJXMsQCGDGonWZwb\r\ntRnHgXTJBnCQDurkyIzq\r\nmcWDyzYHJFtbvumKxDtg\r\nUfaCVcNLlghiBNTiuaTw\r\ndGiMFeETZDyNQgiVuALP\r\nHIJkKGBQkZcaNXUKtugw\r\nHwzaaWRbgKLxfBevAaEj\r\nfiggRZAUnmWIOCKmmuER\r\nPIRAnoPIZBLuorhQxBBq\r\nxKOiOFxRwTnzPEXxzLLJ\r\nxQHSCGYnvGoMfqdccgKi\r\nnqAMjtUafUQomlOoToQt\r\nJGquMGNbHmgRLXjspBbX\r\nbCKFmgHPdBzFZalqCiUL\r\nfrHwPAotKvsUEHDgDxly\r\njOpXetuAIkxDIUMYvscb\r\ntrSfYdURkpefhgtUMTbX\r\nMgMEleOZyPKLAyTQwVlM\r\nBRFiLkVfPUiqMdcHGzZN\r\nbDLJIVUZpWZhoTIewfWz\r\niaVFNnbuyymsRlgHRSyi\r\nOJSHEuDwJNXeEzgtdRvg\r\nIhoitYtoOSuXAJVZpbOF\r\nQIHfDRIknEeJRwMTwziJ\r\nTwgKDBCrpYjmFhSjFSQQ\r\niLCOQRwidJrFxJQNALDy\r\ntkYNPaSRuucfzwkyGQvN\r\nAQBWMTlaoYWPPKhaqwTQ\r\nQeGqWeAQHrHxWwRQzkbq\r\nUYbiDROZGcdnaQhjmJKC\r\nSCnKEEgKHXTvdLHGHlfI\r\nPiCViqesSfymHLpVRxoq\r\nHOkLXGSFIdpjEUJCmPYS\r\nRIFdZpfsyeeXUjhjVsKI\r\nbAAxUEftetTeUfUWbZWY\r\nWIWAIjJaMnxOzZyBDKDm\r\nQBKDirZZokCeMCGoJcgH\r\nYprLQGEQnIRZsszdkOvK\r\npvZMOywmmDfvYsGwLFQh\r\nLmnRwPLllbfCXuaSIJtN\r\nIkYpXfxppyjLlRzWrgaY\r\nrjLEjAvFHIQWHwDxuyaq\r\nEPXpsJahHckTwGQOtxDn\r\nVQGUrXTmeiyrSXzpkSaY\r\nFywiHpxPQIMeZPxvTlFW\r\nOZvdJCqbrPcEgpKzIHBq\r\nDvfVAbCgGWRHEmodpATv\r\nMGgYicaAoFXUngvjVall\r\nsioCawDtqFVHLaQzPJYM\r\nLMlYzhvjMwpITZgZXMIu\r\nOaXueMMqpYJtKEZtGLMA\r\nmCAhNyUvuGMAlmaxvLAl\r\nKgyjVLuOjzjUfyMcXJZa\r\nBghzMAesQeovBFdwfojl\r\nDCIDPyEErWvMbXYuLHtf\r\nhSspvMNVtnMAEEJXGUZQ\r\nITJYbskDxHjaSCrOyXnD\r\nRySkNGqKPCMExxgLZQRw\r\nxkciDYfgyflrQDqorgoL\r\nnKwbsMgXbOndwoAYTIGG\r\nSeJsDccnsKqGfYvBEKfV\r\nXRmfLYRXLQGUfHQHozye\r\nypXkRXPjtsRRZcRvLNTL\r\nlwKJxIivcLoOHRoJvhtj\r\nUsSZjSDbBzHMeUNGyCPp\r\nBmURLQXjCJOMGLlgYQMS\r\nrcqjmEYSPEmkjMbXDXad\r\nMuVZsryhHgNEchqxtEUH\r\nEnZLvafBEcvbVsfvLVqx\r\nqzTRPgGdMinSgJxirNoc\r\nKgewUWlaqXymettrzkjw", "47205699860", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Name! Maximum 150 characteres!");

    }

    [Fact(DisplayName = "Create Person without document")]
    public void CreatePerson_WithoutDocument_DomainExceptionDocumentRequired()
    {
        Action action = () => new Person(1, "Julio", "", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Document is required!");
    }

    [Fact(DisplayName = "Create Person with null document")]
    public void CreatePerson_WithNullDocument_DomainExceptionDocumentRequired()
    {
        Action action = () => new Person(1, "Julio", null, "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Document is required!");
    }

    [Fact(DisplayName = "Create Person with short document")]
    public void CreatePerson_WithShortDocument_DomainExceptionShortDocument()
    {
        Action action = () => new Person(1, "Julio", "123456", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Document! Document must have 11 characteres!");
    }

    [Fact(DisplayName = "Create Person with long document")]
    public void CreatePerson_WithLongDocument_DomainExceptionLongDocument()
    {
        Action action = () => new Person(1, "Julio", "4720569986060", "11975701286");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Document! Document must have 11 characteres!");
    }

    [Fact(DisplayName = "Create Person without phone")]
    public void CreatePerson_WithoutPhone_DomainExceptionDocumentRequired()
    {
        Action action = () => new Person(1, "Julio", "47205699860", "");
        action.Should().Throw<DomainValidationException>().WithMessage("Phone is required!");
    }


    [Fact(DisplayName = "Create Person with null phone")]
    public void CreatePerson_WithNullPhone_DomainExceptionDocumentRequired()
    {
        Action action = () => new Person(1, "Julio", "47205699860", null);
        action.Should().Throw<DomainValidationException>().WithMessage("Phone is required!");
    }

    [Fact(DisplayName = "Create Person with short phone")]
    public void CreatePerson_WithShortPhone_DomainExceptionShortPhone()
    {
        Action action = () => new Person(1, "Julio", "47205699860", "1197570");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Phone! Minimum 11 characteres!");
    }

    [Fact(DisplayName = "Create Person with long phone")]
    public void CreatePerson_WithLongPhone_DomainExceptionLongPhone()
    {
        Action action = () => new Person(1, "Julio", "47205699860", "119757012866060");
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Phone! Maximum 13 characteres!");
    }

}
