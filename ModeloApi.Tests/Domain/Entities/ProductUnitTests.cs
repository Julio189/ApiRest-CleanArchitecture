
using FluentAssertions;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Validations;

namespace ModeloApi.Tests.Domain.Entities;
public class ProductUnitTests
{
    [Fact(DisplayName = "Create Product with valid state")]
    public void CreateProduct_WithValidParameters_ResultObjectValidState()
    {
        Action action = () => new Product(1, "PS5", "1501286", 3500);
        action.Should().NotThrow<DomainValidationException>();
    }

    [Fact(DisplayName = "Create Product with negative Id")]
    public void CreateProduct_WithNegativeId_DomainExceptionInvalidId()
    {
        Action action = () => new Product(-1, "PS5", "47205699860", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Id!");
    }

    [Fact(DisplayName = "Create Product without name")]
    public void CreateProduct_WithoutNameValue_DomainExceptionNameRequired()
    {
        Action action = () => new Product(1, "", "47205699860", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Name is required!");
    }

    [Fact(DisplayName = "Create Product with null name")]
    public void CreateProduct_WithNullName_DomainExceptionNameRequired()
    {
        Action action = () => new Product(1, null, "47205699860", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Name is required!");
    }

    [Fact(DisplayName = "Create Product with short name")]
    public void CreateProduct_WithShortName_DomainExceptionShortName()
    {
        Action action = () => new Product(1, "J", "47205699860", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Name! Minimum 2 characteres!");
    }

    [Fact(DisplayName = "Create Product with long name")]
    public void CreateProduct_WithLongName_DomainExceptionLongName()
    {
        Action action = () => new Product(1, "jimRQojLCVsKWErNTtOk\r\npjPUsQvDrChBSSUqNAmk\r\nTTpMEMlBzwqXKNfSTJie\r\nUunQNBFiZCOXRNdAxbLv\r\naRBYDBgbIjauzkJOZSjG\r\nxSVJYjhmiBiFHEJYAkNE\r\nMUmVIuCdKRjqREJNWTmM\r\nXmbALeNMPHMZJMoeqQUY\r\nWHQrCuUdirZHKNEaLDgn\r\nXmsFeVltoyOOVPRhLdNG\r\nJZmXNiVHfwHQRIbDoWPh\r\nzqzWKxIxRZxcJWmzmRIl\r\nSSkOednsvVhUVCwsZfmd\r\nYYDlbKKrRLNZvRvCqQnb\r\nBVpfyXUJEKILFgXkUHCg\r\nsFealupAQMQKqXCyALEf\r\npoJxgAfbCoUfVTRYAZUM\r\nZAWviGpXHtkKtBosVfQr\r\nBKOqwhypHygeiizoAjMO\r\ntxeeRxjKKWuFPsmrLnWN\r\nicZhTGLofzPdOtdBsUmM\r\ncFawIhjHfiJvIoiKscpl\r\nlTWoXrnQzvWSUlxHeUlh\r\nHaGGRrsOBFKlvLcFOxti\r\njPxShQGdLNHQDFqgbebh\r\nnWMAdTvLUlyrLpsaeaMY\r\nWrgBGBAGiUYbXyZChCFQ\r\nooUpAsncfVdKhumxbGNW\r\nslRIRkTVCsBdigHbquku\r\nnRqfZEggaQjAUoXQjyXc\r\nQjdHbULKiGaYreHIzULO\r\nhVmwadMVQqWJZFvHHLKE\r\nsRfvAZzMuTeUFeSYLwQK\r\nEExJCekuhvoTEHnqfvvn\r\nnkTeiRqxbYPiCJFjwgVc\r\nqXmcKgaPfeelQPpVbcFP\r\nQknGQWeoSenfKTIQuGvJ\r\nisZZxSamdceDzaiGDDNZ\r\nksnYIthafOaMmjFcBwXu\r\nPAkRSwhVATGATLWbvZbG\r\nzwmQlCakDOFODbDcdWFE\r\nBvzbmzrwVmiAGZEVsHQC\r\nhdOycjHPGFxXgHxrbUJL\r\nLZYVIUFmDYniQHdvaAFu\r\nDxIgUNtRomKQpBnJgeKz\r\njcFbnMRDbpWMfSROBSLl\r\nxBIdcngXPVRTCKTnaVuU\r\nxfWSdvKgtdlOgYDxKnMi\r\nPCcqfZZVnVontZLlFvzq\r\nzKHtGRzGBWGIOiJfsvyU\r\nXqTcVbWaggnaTrLpyWQZ\r\nIBhoIbSDvMfMUFilHpmf\r\nOiAvNOXIBNzBUOpXNtux\r\nUGWKKoPlOiXbMYLMHJbL\r\nIKbqaCSDOhnSWVellSYH\r\nNcJyKItkboWLBdgWhOBJ\r\nsUTSuefKIDdVgcnIeYGx\r\nsPZRZTrdibXutgrUGguk\r\nQTETAdemyftJJRXcqepl\r\nUhkXDAbAgtuIQHIZPuNC\r\nsbiDpPhfGubAIOvJrtIq\r\npSZCvMSUUdyxLuGyonKy\r\nDgcjnkTmVVdYZXdKgXqd\r\nIzzvHLyrvNaHRnKEdQXS\r\njAhmsTKXbjcFjTuHmmCk\r\nkeKVeUNGMVToeTNzxMCC\r\nYJQlPYDoKXOgdQQeqMYd\r\nekaFjHIzcvzpjyensfJB\r\nwgmKIrJdwyHmcvgzekKZ\r\nNpAOIEhJGTktaIgWvJBQ\r\niMnZXbAEtCXnpQYZTrRO\r\nuBJlWxkhtcqNIAxYPoMp\r\nqnnjUnGozKPfWYhPpuOO\r\niRqurGvfuhmXZSNzBglH\r\nerouaXlMdAMBMmKakzcM\r\nKvfTGMKOcEOPQzIgrMUL\r\npijPSkzEUzNSigRAxPro\r\nuqDUaGCZQJRqXhVvidjd\r\nLYpGbmPAVCNaLCcTfEoM\r\nHTUYRoXGTjaSeYGQxszP\r\nndgcfJXMsQCGDGonWZwb\r\ntRnHgXTJBnCQDurkyIzq\r\nmcWDyzYHJFtbvumKxDtg\r\nUfaCVcNLlghiBNTiuaTw\r\ndGiMFeETZDyNQgiVuALP\r\nHIJkKGBQkZcaNXUKtugw\r\nHwzaaWRbgKLxfBevAaEj\r\nfiggRZAUnmWIOCKmmuER\r\nPIRAnoPIZBLuorhQxBBq\r\nxKOiOFxRwTnzPEXxzLLJ\r\nxQHSCGYnvGoMfqdccgKi\r\nnqAMjtUafUQomlOoToQt\r\nJGquMGNbHmgRLXjspBbX\r\nbCKFmgHPdBzFZalqCiUL\r\nfrHwPAotKvsUEHDgDxly\r\njOpXetuAIkxDIUMYvscb\r\ntrSfYdURkpefhgtUMTbX\r\nMgMEleOZyPKLAyTQwVlM\r\nBRFiLkVfPUiqMdcHGzZN\r\nbDLJIVUZpWZhoTIewfWz\r\niaVFNnbuyymsRlgHRSyi\r\nOJSHEuDwJNXeEzgtdRvg\r\nIhoitYtoOSuXAJVZpbOF\r\nQIHfDRIknEeJRwMTwziJ\r\nTwgKDBCrpYjmFhSjFSQQ\r\niLCOQRwidJrFxJQNALDy\r\ntkYNPaSRuucfzwkyGQvN\r\nAQBWMTlaoYWPPKhaqwTQ\r\nQeGqWeAQHrHxWwRQzkbq\r\nUYbiDROZGcdnaQhjmJKC\r\nSCnKEEgKHXTvdLHGHlfI\r\nPiCViqesSfymHLpVRxoq\r\nHOkLXGSFIdpjEUJCmPYS\r\nRIFdZpfsyeeXUjhjVsKI\r\nbAAxUEftetTeUfUWbZWY\r\nWIWAIjJaMnxOzZyBDKDm\r\nQBKDirZZokCeMCGoJcgH\r\nYprLQGEQnIRZsszdkOvK\r\npvZMOywmmDfvYsGwLFQh\r\nLmnRwPLllbfCXuaSIJtN\r\nIkYpXfxppyjLlRzWrgaY\r\nrjLEjAvFHIQWHwDxuyaq\r\nEPXpsJahHckTwGQOtxDn\r\nVQGUrXTmeiyrSXzpkSaY\r\nFywiHpxPQIMeZPxvTlFW\r\nOZvdJCqbrPcEgpKzIHBq\r\nDvfVAbCgGWRHEmodpATv\r\nMGgYicaAoFXUngvjVall\r\nsioCawDtqFVHLaQzPJYM\r\nLMlYzhvjMwpITZgZXMIu\r\nOaXueMMqpYJtKEZtGLMA\r\nmCAhNyUvuGMAlmaxvLAl\r\nKgyjVLuOjzjUfyMcXJZa\r\nBghzMAesQeovBFdwfojl\r\nDCIDPyEErWvMbXYuLHtf\r\nhSspvMNVtnMAEEJXGUZQ\r\nITJYbskDxHjaSCrOyXnD\r\nRySkNGqKPCMExxgLZQRw\r\nxkciDYfgyflrQDqorgoL\r\nnKwbsMgXbOndwoAYTIGG\r\nSeJsDccnsKqGfYvBEKfV\r\nXRmfLYRXLQGUfHQHozye\r\nypXkRXPjtsRRZcRvLNTL\r\nlwKJxIivcLoOHRoJvhtj\r\nUsSZjSDbBzHMeUNGyCPp\r\nBmURLQXjCJOMGLlgYQMS\r\nrcqjmEYSPEmkjMbXDXad\r\nMuVZsryhHgNEchqxtEUH\r\nEnZLvafBEcvbVsfvLVqx\r\nqzTRPgGdMinSgJxirNoc\r\nKgewUWlaqXymettrzkjw", "47205699860", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Name! Maximum 150 characteres!");

    }

    [Fact(DisplayName = "Create Product without Cod Erp")]
    public void CreatePerson_WithoutCodErp_DomainExceptionCodErpRequired()
    {
        Action action = () => new Product(1, "PS5", "", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Cod Erp is required!");
    }

    [Fact(DisplayName = "Create Product with null Cod Erp")]
    public void CreatePerson_WithNullCodErp_DomainExceptionCodErpRequired()
    {
        Action action = () => new Product(1, "PS5", null, 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Cod Erp is required!");
    }

    [Fact(DisplayName = "Create Product with short Cod Erp")]
    public void CreatePerson_WithShortCodErp_DomainExceptionShortCodErp()
    {
        Action action = () => new Product(1, "PS5", "12", 3500);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Cod Erp! Minimum 3 characteres!");
    }

    [Fact(DisplayName = "Create Product with negative price")]
    public void CreatePerson_WithNegativePrice_DomainExceptionInvalidPrice()
    {
        Action action = () => new Product(1, "PS5", "1501286", -35);
        action.Should().Throw<DomainValidationException>().WithMessage("Invalid Price!");
    }
}
