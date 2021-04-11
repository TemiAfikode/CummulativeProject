// AJAX for teacher Add can go in here!
// This file is connected to the project via Shared/_Layout.cshtml


function AddTeacher() {

	//goal: send a request which looks like this:
	//POST : https://localhost:44346/api/TeacherData/AddTeacher
	//with POST data of teacherfname, teacherlname, employeenumber, hiredate, salary.

	var URL = "https://localhost:44346/api/TeacherData/AddTeacher/";

	var rq = new XMLHttpRequest();
	// where is this request sent to?
	// is the method GET or POST?
	// what should we do with the response?

	var TeacherFname = document.getElementById('TeacherFname').value;
	var TeacherLname = document.getElementById('TeacherLname').value;
	var Employeenumber = document.getElementById('Employeenumber').value;
	var Hiredate = document.getElementById('Hiredate').value;
	var Salary = document.getElementById('Salary').value;




	var TeacherData = {
		"TeacherFname": TeacherFname,
		"TeacherLname": TeacherLname,
		"Employeenumber": Employeenumber,
		"Hiredate": Hiredate,
		"Salary": Salary,

	};


	rq.open("POST", URL, true);
	rq.setRequestHeader("Content-Type", "application/json");
	rq.onreadystatechange = function () {
		//ready state should be 4 AND status should be 200
		if (rq.readyState == 4 && rq.status == 200) {
			//request is successful and the request is finished

			//nothing to render, the method returns nothing.


		}

	}
	//POST information sent through the .send() method
	rq.send(JSON.stringify(TeacherData));
	window.location ="https://localhost:44346/Teacher/List"

}