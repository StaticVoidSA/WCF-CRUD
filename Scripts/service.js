/// <reference path="jquery-3.1.1.min.js" />

function getAllHeroes() {
    $.ajax({
        url: "Service/SuperHeroService.svc/GetAllHeroes",
        type: "GET",
        dataType: "json",
        success: function (result) {
            heroes = result;
            drawHeroTable(result);
        }
    });
}

function addHero() {
    var newHero = {
        "FirstName": $("#addFirstname").val(),
        "LastName": $("#addLastname").val(),
        "HeroName": $("#addHeroname").val(),
        "PlaceOfBirth": $("#addPlaceOfBirth").val(),
        "Combat": $("#addCombatPoints").val()
    };

    $.ajax({
        url: "Service/SuperHeroService.svc/AddHero",
        type: "POST",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(newHero),
        success: function () {
            showOverview();
        }
    });
}

function putHero() {
    updateHero.FirstName = $("#updateFirstname").val();
    updateHero.LastName = $("#updateLastname").val();
    updateHero.HeroName = $("#updateHeroname").val();
    updateHero.PlaceOfBirth = $("#updatePlaceOfBirth").val();
    updateHero.Combat = $("#updateCombatPoints").val();

    $.ajax({
        url: "Service/SuperHeroService.svc/UpdateHero/" + updateHero.Id,
        type: "PUT",
        dataType: "json",
        contentType: "application/json",
        data: JSON.stringify(updateHero),
        success: function () {
            showOverview();
        }
    });
}

function deleteHero(heroId) {
    $.ajax({
        url: "Service/SuperHeroService.svc/DeleteHero/" + heroId,
        type: "DELETE",
        dataType: "json",
        success: function () {
            getAllHeroes();
        }
    });
}

function searchHero() {
    var searchText = $("#searchText").val();

    $.ajax({
        url: "Service/SuperHeroService.svc/SearchHero/" + searchText,
        type: "GET",
        dataType: "json",
        success: function (result) {
            heroes = result;
            drawHeroTable(result);
        },
        error: function (error) {
            console.error(error.responseText);
            $("#heroOverview").html(error.responseText);
        }
    });
}

function sortedHeroList(type) {
    $.ajax({
        url: "Service/SuperHeroService.svc/GetSortedHeroList/" + type,
        type: "GET",
        dataType: "json",
        success: function (result) {
            drawHeroTable(result);
        }
    });
}

function fight() {
    var id1 = $("#fighter1").val();
    var id2 = $("#fighter2").val();

    $.ajax({
        url: "Service/SuperHeroService.svc/Fight/" + id1 + "/" + id2,
        type: "GET",
        dataType: "json",
        success: function (result) {
            $('#fightResult').html(result);
        }
    });
}