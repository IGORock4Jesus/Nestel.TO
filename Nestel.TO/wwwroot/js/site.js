// Nestel.LLC*

// мой запрос на получение страницы
function make_get(path, params) {
	console.log("Get-запрос " + path);

	$.get(path, function (data) {
		$("#commonView").html(data);
	})
		.fail(function (message) {
			alert(message);
		});
}

// post-зопрос 
function make_post(path, data) {
	console.log("path = " + path);
	console.log("data = " + data);
	//$.post(path, { demo: data }, function (retData, status) {
	//	console.log("status: " + status);
	//	$("#commonView").html(retData);
	//});
	//$.post(path, data)
	//	.fail(function (message) {
	//		alert(message);
	//	});
}

// Запрашивает список пользователей
function show_user_list() {
	// получаем представление
	make_get("/Users/List");
}

// зопрос представления создание пользователя
function show_create_user() {
	// получаем представление
	make_get("/Users/Create");
}

// зопрос представления создание пользователя
function show_edit_user(id) {
	// получаем представление
	//make_get("/Users/Edit");
	$.ajax({
		url: "/Users/Edit",
		data: {
			id: id
		},
		type: "GET",
		success: function (data) {
			$("#commonView").html(data);
		},
		error: function (message) {
			console.log(message);
		}
	});
}

// спросить у пользователя - вы уверены
function show_delete_user(id) {
	$.ajax({
		url: "/Users/Delete",
		data: {
			id: id
		},
		type: "GET",
		success: function (data) {
			$("#commonView").html(data);
		},
		error: function (message) {
			console.log(message);
		}
	});
}
// создать пользователя
function create_user() {
	var username = $("#username").val();
	var password = $("#password").val();
	var email = $("#email").val();
	var phone = $("#phone").val();

	$.post("/Users/Create", {
		Username: username,
		Password: password,
		EMail: email,
		Phone: phone
	}, function (retData, status) {
		console.log("status: " + status);
		$("#commonView").html(retData);
	})
		.fail(function (message) {
			console.log(message);
		});
}

// обновление пользователя
function edit_user(id) {
	var username = $("#username").val();
	var email = $("#email").val();
	var phone = $("#phone").val();

	$.ajax({
		url: "/Users/Edit",
		data: {
			Id: id,
			Username: username,
			EMail: email,
			Phone: phone
		},
		type: "POST",
		success: function (retData, status) {
			console.log("status: " + status);
			$("#commonView").html(retData);
		},
		error: function (message) {
			console.log(message);
		}
	});
}

// удалить пользователя
function delete_user(id) {
	$.ajax({
		url: "/Users/Delete",
		data: {
			id: id
		},
		type: "POST",
		success: function (retData, status) {
			console.log("status: " + status);
			$("#commonView").html(retData);
		},
		error: function (message) {
			console.log(message);
		}
	});
}