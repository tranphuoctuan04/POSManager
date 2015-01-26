var AjaxInstance = new AjaxObj();
var AjaxInstanceCreate = new AjaxObj();
var AjaxInstanceEdit = new AjaxObj();
var AjaxInstanceDelete = new AjaxObj();

var #TableName#ViewModel = function () { // Begin ViewModel
    var self = this;
    // Data

    self.val = #TableName#validation;

    self.List = ko.observableArray([]); // 
    self.Current = ko.observable(null);
    self.SearchName = ko.observable("");

    //function
    self.CountSearch = ko.computed(function () {
        var i = 0;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.IsVisible() == true)
                i++;
        });
        return i;
    });


    self.InitData = function () {
        self.InitList();
    };

    self.InitList = function () {
        // AjaxInstance.SuccessHandleFunction
        AjaxInstance.SuccessHandleFunction = function (data) { //
            self.List.removeAll();
            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) { //duyệt  mãng data dạng Json=> item => action 
                    index++;
                    return new #TableName#(#TenCotParameter#); // 
                });
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
        };
        // AjaxInstanceCreate.SuccessHandleFunction
        AjaxInstanceCreate.SuccessHandleFunction = function (data) { //
            if (data == "success") {
                AjaxInstance.Action("", "#TableName#/Get#TableName#Items", 'application/json; charset=utf-8', 'get');
                $('#modal').modal('hide');
                $("#form")[0].reset();
            } else {
                // Xử lý khi fail
                alert(data);
            }
        };
        // AjaxInstanceEdit.SuccessHandleFunction
        AjaxInstanceEdit.SuccessHandleFunction = function (data) { //
            if (data == "success") {
                AjaxInstance.Action("", "#TableName#/Get#TableName#Items", 'application/json; charset=utf-8', 'get');
                $('#modal').modal('hide');
            } else {
                // Xử lý khi fail
                alert(data);
            }
        };
        // AjaxInstanceDelete.SuccessHandleFunction
        AjaxInstanceDelete.SuccessHandleFunction = function (data) { //
            if (data == "success") {
                AjaxInstance.Action("", "#TableName#/Get#TableName#Items", 'application/json; charset=utf-8', 'get');
                self.Current(self.List()[0]);
            } else {
                alert(data);
            }
        };

        AjaxInstance.Action("", "#TableName#/Get#TableName#Items", 'application/json; charset=utf-8', 'get');
    };

    //Action
    self.btnSetCurrent = function (item) {
        item.OldValue(ko.toJS(item));
        self.Current(item);
    };
    self.btnSearchName = function () {
        //alert('search');
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.#SearchField#().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
                if (iCurr == null) {
                    iCurr = item;
                    self.Current(item);
                }
                item.IsVisible(true);
            }
            else {
                item.IsVisible(false);
            }
        });
    };

    
    self.Create#TableName# = function () {
        $(".field-validation-error").empty(); // Xóa validation nếu trước đó có mà người dùng bấm close
        $("#form")[0].reset(); // Reset form lại, làm trống
        
        
        // Hiện nút này, ẩn nút kia.
        $("#btnEdit").hide();
        $("#btnCreate").show();
    };

    self.Create#TableName#Submit = function () {
        $("#form").validate();
        var valid = $("#form").valid();
        if (valid) {
            AjaxInstanceCreate.Action($("#form").serialize(),
            "#TableName#/Create",
            'application/x-www-form-urlencoded; charset=UTF-8',
            'post');
        }
    };

    self.Edit#TableName# = function (item) {
        $(".field-validation-error").empty();

        $("#form")[0].reset();

        $("#btnCreate").hide();
        $("#btnEdit").show();

        // Gán các giá trị vào các Input text cho dễ Edit
        #CotHTML#
    };

    self.Edit#TableName#Submit = function (item) {
        $("#form").validate();
        var valid = $("#form").valid();

        if (valid) { // Nếu form không lỗi (chữ đỏ) thì cho submit lên server
            AjaxInstanceEdit.Action($("#form").serialize(),
           "#TableName#/Edit",
           'application/x-www-form-urlencoded; charset=UTF-8',
           'post');
        }
    }
    self.Delete#TableName#Submit = function (item) {
        // Thư viện 3, xuất thông báo xác nhận chắc chắn muốn xóa
        jConfirm('Bạn có chắc chắn muốn xóa' + "?", 'Thông báo xác nhận', function (confirmResult) {
            if (confirmResult) {  // Nếu người dùng click ok
                AjaxInstanceDelete.Action( // Gửi tin nhắn lên server và xóa
                    $("#formDelete").serialize(),
                    "#TableName#/Delete",
                    'application/x-www-form-urlencoded; charset=UTF-8',
                    'post');
            }
        });
    }
}; // End View Model

$(document).ready(function () {
    var model = new #TableName#ViewModel();

    model.InitData();
    ko.applyBindings(model);
});