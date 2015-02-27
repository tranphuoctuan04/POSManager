var AjaxObj = function () {
    this.SuccessHandleFunction = null;
    this.Action = function (parameters, ajaxUrl, contentType, method) {
        $.ajax({
            type: method,
            url: ajaxUrl,
            async: false,
            contentType: contentType,
            data: parameters,
            success: function (e) {
                if (AjaxInstance.SuccessHandleFunction != null) {
                    AjaxInstance.SuccessHandleFunction(e.Data);
                }
            },
            error: function (e) {
                alert(e.responseText);
                alert('Lỗi không thể kết nối đến server.');
            }
        });

    };
};
var AjaxInstance = new AjaxObj();

var NhomHanghoaViewModel = function () {
    var self = this;
    // Data
    self.List = ko.observableArray([]);
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
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data != null && data.length > 0) {
                var index = 0;
                var listItems = ko.utils.arrayMap(data, function (item) {
                    //alert(item.co_name);
                    index++;
                    return new NhomHanghoa(item.NhomHanghoaId, item.Code, item.Ten);
                });
                // alert(listItems);
                self.List.push.apply(self.List, listItems);
                if (self.List().length > 0)
                    self.Current(self.List()[0]);
            }
        };

        AjaxInstance.Action("", "http://localhost:56173/NhomHanghoa/GetNhomHanghoaItems", 'application/json; charset=utf-8', 'get');
    };

    //Action
    self.btnSetCurrent = function (item) {
        self.Thoat(); // Để phòng trường hợp nếu người dùng ấn edit sửa rồi ấn qua sp khác.
        item.IsEdit(false);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    };
    self.btnSearchName = function () {
        var iCurr = null;
        ko.utils.arrayForEach(self.List(), function (item) {
            if (item.Ten().toLowerCase().indexOf(self.SearchName().toLowerCase()) != -1) {
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
    self.CreateHanghoa = function () {
        self.Thoat();
        var newItem = new NhomHanghoa(null, null, null);
        newItem.IsEdit(true);
        newItem.IsCreate(true);
        self.Current(newItem);
    };

    self.Thoat = function (item) {
        if (!item)
            item = self.Current();

        if (!item.OldValue())
            return;

        var obj = JSON.parse(ko.toJSON(item.OldValue())); // Lấy oldValue
        if (obj.NhomHanghoaId) {// Nếu có Id Nghĩa là người dùng ấn Edit rồi hũy   
            item.NhomHanghoaId(obj.NhomHanghoaId);
            item.Code(obj.Code);
            item.Ten(obj.Ten)
        }
        item.IsEdit(false);
        self.Current(self.List()[0]);
    };

    self.Edit = function (item) {
        self.Thoat();
        item.IsEdit(true);
        item.OldValue(ko.toJS(item));
        self.Current(item);
    }

    self.luuHanghoa = function (item) {
        if (!self.Current().isValid()) // Kiểm tra xem model này có đúng hay không
            return // Nếu sai thì return.

         //Kiểm tra xem code có trùng không?
        var codeTrung = false;
        ko.utils.arrayForEach(self.List(), function (val) {
            if (val.Code() == item.Code() && val != item) {
                alert("Code \"" + val.Code() + "\" đã tồn tại cho nhóm hàng hóa \"" + val.Ten() + "\".");
                codeTrung = true;
                return;
            }
        });
        if (codeTrung)
            return;

        var jsonData = ko.toJSON(self.Current());
        if (self.Current().IsCreate() == false) {
            AjaxInstance.SuccessHandleFunction = function (data) {
                if (data == "success") {
                    alert("Sửa thành công");
                    self.Current().OldValue(null);
                } else {
                    alert(data);
                }
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/NhomHanghoa/EditNhomHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        else {
            AjaxInstance.SuccessHandleFunction = function (data) {
                var arr = data.split('-+-');
                if (arr[0] == "success") {
                    item.NhomHanghoaId(arr[1]);
                    self.List.push(item);
                    alert("Thêm thành công");
                } else {
                    alert(data);
                }
                
            }
            AjaxInstance.Action(jsonData, "http://localhost:56173/NhomHanghoa/addNhomHanghoaItem", 'application/json; charset=utf-8', 'post');
        }
        self.Current().IsCreate(false);
        self.Current().IsEdit(false);
    };
    self.remove = function (item) {
        if (confirm("Bạn có chắc chắn muốn xóa: " + item.Code()) == false) {
            return;
        }
        var jsonData = ko.toJSON(item);
        AjaxInstance.SuccessHandleFunction = function (data) {
            if (data == "success") {
                self.List.remove(item);
                alert("Xóa thành công")
            }
            else {
                alert(data);
            }         
        }
        AjaxInstance.Action(jsonData, "http://localhost:56173/NhomHanghoa/deleteNhomHanghoaItem", 'application/json; charset=utf-8', 'post');

        self.Current(self.List()[0]);
    }
};

$(document).ready(function () {
    var model = new NhomHanghoaViewModel();
    model.InitData();

    ko.validation.init({
        decorateElement: true,
        errorMessageClass: 'text-error',
        insertMessages: true,
    });
    ko.applyBindings(model);
});

