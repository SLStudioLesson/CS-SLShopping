namespace SLShopping.Tests;

using Xunit;
using SLShopping.Controllers;
using SLShopping.Models;
using SLShopping.Data;
using SLShopping.ViewModels;
using Microsoft.AspNetCore.Mvc;

public class BrandsControllerTests : IClassFixture<DatabaseFixture>
{
    private readonly ApplicationDbContext _context;
    private readonly BrandsController _brandsController;

    public BrandsControllerTests(DatabaseFixture fixture)
    {
        _context = fixture.Context;
        _brandsController = new BrandsController(_context);
    }

    [Fact(DisplayName = "ブランド管理_一覧画面（全件）")]
    public async void Brands_Index_View_All()
    {
        // Arrange（準備）
        // ブランドデータの件数を変数で持っておく
        var expectedBrandsCount = _context.Brands.Count();

        // Act（実行）
        // BrandsControllerのIndex()メソッドを実行し、結果を変数resultに格納する
        var result = await _brandsController.Index();

        // Assert（検証）
        // resultがViewResult型であるか検証し、ViewResultとしてキャストする
        // viewResult.ModelがBrandSearchViewModel型であるかの検証し、BrandSearchViewModelとしてキャスト
        // ブランド情報の件数が期待値通りか検証
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsType<BrandSearchViewModel>(viewResult.Model);
        Assert.Equal(expectedBrandsCount, model.Results.Count);
    }

    [Fact(DisplayName = "ブランド管理_一覧画面（検索結果）")]
    public async void Brands_Index_View_SearchResults()
    {
        // Arrange（準備）
        // 検索ワード "Urban" を使用し、検索に一致するブランドデータの件数をカウント
        var searchWord = "Urban";
        var expectedBrandsCount = _context.Brands.Where(b => b.Name.Contains(searchWord)).Count();

        // 検索ワードをBrandSearchViewModelに設定
        var _brandSearchViewModel = new BrandSearchViewModel { Name = searchWord };

        // Act（実行）
        // BrandsControllerのIndex()メソッドを検索条件付きで実行し、結果を変数resultに格納する
        var result = await _brandsController.Index(_brandSearchViewModel);

        // Assert（検証）
        // resultがViewResult型であるか検証し、ViewResultとしてキャスト
        var viewResult = Assert.IsType<ViewResult>(result);

        // viewResult.ModelがBrandSearchViewModel型であるか検証し、BrandSearchViewModelとしてキャスト
        var model = Assert.IsType<BrandSearchViewModel>(viewResult.Model);

        // 検索結果のブランド情報の件数が期待値通りか検証
        Assert.Equal(expectedBrandsCount, model.Results.Count);
    }

    [Fact(DisplayName = "ブランド管理_詳細画面")]
    public async void Brands_Details_View()
    {
        // Arrange（準備）
        // 詳細画面で表示するブランドIDを指定
        var brandId = 5;

        // Act（実行）
        // BrandsControllerのDetails()メソッドを指定したブランドIDで実行し、結果を変数resultに格納する
        var result = await _brandsController.Details(brandId);

        // Assert（検証）
        // resultがViewResult型であるか検証し、ViewResultとしてキャスト
        var viewResult = Assert.IsType<ViewResult>(result);

        // viewResult.ModelがBrand型であるか検証し、Brandとしてキャスト
        var model = Assert.IsType<Brand>(viewResult.Model);

        // 取得したブランドのIDが期待値通りか検証
        Assert.Equal(brandId, model.Id);
    }

    [Fact(DisplayName = "ブランド管理_詳細画面（Not Found）")]
    public async void Brands_Details_View_NotFound()
    {
        // Arrange（準備）
        // 存在しないブランドIDを指定
        int nonExistentBrandId = -999;

        // Act（実行）
        // BrandsControllerのDetails()メソッドを指定したブランドIDで実行し、結果を変数resultに格納する
        var result = await _brandsController.Details(nonExistentBrandId);

        // Assert（検証）
        // resultがNotFoundResult型であるか検証
        Assert.IsType<NotFoundResult>(result);
    }


    [Fact(DisplayName = "ブランド管理_新規登録画面")]
    public void Brands_Create_View()
    {
        // Act（実行）
        // BrandsControllerのCreate()メソッドを実行し、新規登録画面の結果を変数resultに格納
        var result = _brandsController.Create();

        // Assert（検証）
        // resultがViewResult型であるか検証
        Assert.IsType<ViewResult>(result);
    }

    [Fact(DisplayName = "ブランド管理_新規登録（Execute）")]
    public async void Brands_Create_Execute()
    {
        // Arrange（準備）
        // 新規ブランド情報を作成し、現在のブランド件数に1を加えた値を期待値として設定
        var newBrand = new Brand { Id = 11, Name = "Test Brand" };
        var expectedBrandsCount = _context.Brands.Count() + 1;

        // Act（実行）
        // BrandsControllerのCreate()メソッドを新規ブランド情報で実行し、結果を変数resultに格納
        var result = await _brandsController.Create(newBrand);

        // Assert（検証）
        // resultがRedirectToActionResult型であるか検証（正常に登録が行われたか）
        Assert.IsType<RedirectToActionResult>(result);

        // 新規ブランド登録後のブランド件数が期待値通りか検証
        Assert.Equal(expectedBrandsCount, _context.Brands.Count());
    }

    [Fact(DisplayName = "ブランド管理_編集画面")]
    public async void Brands_Edit_View()
    {
        // Arrange（準備）
        // 編集するブランドIDを指定
        var brandId = 5;

        // Act（実行）
        // BrandsControllerのEdit()メソッドを指定したブランドIDで実行し、結果を変数resultに格納
        var result = await _brandsController.Edit(brandId);

        // Assert（検証）
        // resultがViewResult型であるか検証し、ViewResultとしてキャスト
        var viewResult = Assert.IsType<ViewResult>(result);

        // viewResult.ModelがBrand型であるか検証し、Brandとしてキャスト
        var model = Assert.IsType<Brand>(viewResult.Model);

        // 編集対象ブランドのIDが期待値通りか検証
        Assert.Equal(brandId, model.Id);
    }

    [Fact(DisplayName = "ブランド管理_編集画面（NotFound）")]
    public async void Brands_Edit_View_NotFound()
    {
        // Arrange（準備）
        // 存在しないブランドIDを指定
        int nonExistentBrandId = -999;

        // Act（実行）
        // BrandsControllerのEdit()メソッドを指定したブランドIDで実行し、結果を変数resultに格納
        var result = await _brandsController.Edit(nonExistentBrandId);

        // Assert（検証）
        // resultがNotFoundResult型であるか検証
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact(DisplayName = "ブランド管理_編集（Execute）")]
    public async void Brands_Edit_Execute()
    {
        // Arrange（準備）
        // 編集するブランドIDと新しいブランド名を指定
        var brandId = 1;
        var expectedBrandName = "SLShopping Originals";

        // 対象ブランドをデータベースから取得し、ブランド名を変更
        var editBrand = _context.Brands.Find(brandId)!;
        editBrand.Name = expectedBrandName;

        // Act（実行）
        // BrandsControllerのEdit()メソッドを指定したブランド情報で実行し、結果を変数resultに格納
        var result = await _brandsController.Edit(editBrand.Id, editBrand);

        // データベースから更新されたブランド名を取得
        var resultBrandName = _context.Brands.Find(brandId)?.Name;

        // Assert（検証）
        // resultがRedirectToActionResult型であるか検証（正常に編集が行われたか）
        Assert.IsType<RedirectToActionResult>(result);

        // 更新されたブランド名が期待値通りか検証
        Assert.Equal(expectedBrandName, resultBrandName);
    }
    [Fact(DisplayName = "ブランド管理_削除画面")]
    public async void Brands_Delete_View()
    {
    }

    [Fact(DisplayName = "ブランド管理_削除画面（Not Found）")]
    public async void Brands_Delete_View_NotFound()
    {
    }

    [Fact(DisplayName = "ブランド管理_削除（Execute）")]
    public async void Brands_Delete_Execute()
    {
    }

}