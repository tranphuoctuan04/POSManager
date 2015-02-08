    var StudentModel = function (StudentId, StudentName, StudentInformation, StudentGrade){
var self=this;
self.StudentId = ko.observable(StudentId).extend({});
self.StudentName = ko.observable(StudentName).extend({required: {params: False,message: 'Ten sinh vien khong duoc trong'},});
self.StudentInformation = ko.observable(StudentInformation).extend({});
self.StudentGrade = ko.observable(StudentGrade).extend({min: {params: 5,message: 'Khong duoc nho hon 5'},max: {params: 100,message: 'Nho hon 100'},required: {params: False,message: 'Nam hoc khong duoc de trong'},});
self.IsCreate = ko.observable(false);
                            self.IsEdit = ko.observable(false);
                            self.IsVisible = ko.observable(true);
                            self.OldValue = ko.observable({});

                            // Validate.
                            this.Errors = ko.validation.group(this);
                            this.isValid = ko.computed(function () {
                                return self.Errors().length == 0;
                            });
}