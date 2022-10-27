const lastnameInput = document.getElementById('lastnameInput0');
const nameInput = document.getElementById('nameInput0');
const surnameInput = document.getElementById('surnameInput0');
const phoneInput = document.getElementById('phoneInput0');
const studentsTable = document.getElementById('studentsTable');

const limit = 5;

const listLinks = {
    FirstPageLink: null,
    LastPageLink: null,
    NextPageLink: null,
    PreviousPageLink: null,
    CurrentPageLink: null,
}

const handleCreateClick = async () => {
    const studentCreateData = {
        LastName: lastnameInput.value,
        Name: nameInput.value,
        Surname: surnameInput.value,
        Phone: phoneInput.value,
    };

    const studentResource = await createStudent(studentCreateData);
};

const handleDeleteClick = async (url) => {
    await deleteStudent(url);

    const studentsResource = await loadStudentsResource(listLinks.CurrentPageLink);
    buildTable(studentsResource.Embedded);
};

const handleUpdateClick = async (studentId, url) => {
    const lastname = document.getElementById(`lastnameInput${studentId}`).value;
    const name = document.getElementById(`nameInput${studentId}`).value;
    const surname = document.getElementById(`surnameInput${studentId}`).value;
    const phone = document.getElementById(`phoneInput${studentId}`).value;

    const studentUpdateData = {
        LastName: lastname,
        Name: name,
        Surname: surname,
        Phone: phone,
    };

    await updateStudent(studentUpdateData, url);
};

const handlePager = async (action) => {
    let url = '';
    switch (action) {
        case 'first':
            url = listLinks.FirstPageLink;
            break;
        case 'previous':
            url = listLinks.PreviousPageLink;
            break;
        case 'next':
            url = listLinks.NextPageLink;
            break;
        case 'last':
            url = listLinks.LastPageLink;
            break;
        default:
            break;
    }

    const studentsResource = await loadStudentsResource(url);

    buildTable(studentsResource.Embedded);
    setListLinks(studentsResource.Links, url);
}

async function createStudent(studentCreateData) {
    const requestOptions = {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(studentCreateData),
    };

    const response = await fetch('http://localhost:44367/api/students', requestOptions);
    return await response.json();
}

async function loadStudentsResource(url) {
    const requestOptions = {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json',
        },
    };

    const response = await fetch(url, requestOptions);
    return await response.json();
}

async function deleteStudent(url) {
    const requestOptions = {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json',
        },
    };

    await fetch(url, requestOptions);
}

async function updateStudent(studentUpdateData, url) {
    const requestOptions = {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(studentUpdateData),
    };

    await fetch(url, requestOptions);
}

function buildTable(students) {
    const studentsTableHeader = `
    <tr><th>Lastname</th><th>Name</th><th>Surname</th><th>Phone</th><th>Actions</th></tr>
    <tr>
        <td><input type="text" class="form-control" id="lastnameInput0" placeholder="Lastname"></td>
        <td><input type="text" class="form-control" id="nameInput0" placeholder="Name"></td>
        <td><input type="text" class="form-control" id="surnameInput0" placeholder="Surname"></td>
        <td><input type="text" class="form-control" id="phoneInput0" placeholder="Phone"></td>
        <td><button class="btn btn-success" onclick="handleCreateClick()">Create</button></td>
    </tr>`;
    const studentsTableBody = document.createElement("tbody");
    studentsTableBody.innerHTML = studentsTableHeader;

    students.forEach(student => {
        const row = document.createElement("tr");
        row.innerHTML = `<input id="${student.Id}" type="hidden">`;

        const cell1 = document.createElement('td');
        cell1.innerHTML = `<input type="text" class="form-control" id="lastnameInput${student.Id}" placeholder="Lastname" value="${student.Lastname}">`;

        const cell2 = document.createElement('td');
        cell2.innerHTML = `<input type="text" class="form-control" id="nameInput${student.Id}" placeholder="Name" value="${student.Name}">`;

        const cell3 = document.createElement('td');
        cell3.innerHTML = `<input type="text" class="form-control" id="surnameInput${student.Id}" placeholder="Surname" value="${student.Surname}">`;

        const cell4 = document.createElement('td');
        cell4.innerHTML = `<input type="text" class="form-control" id="phoneInput${student.Id}" placeholder="Phone" value="${student.Phone}">`;

        const deleteUrl = student.Links.find(x => x.Rel === 'delete-it').Href;
        const updateUrl = student.Links.find(x => x.Rel === 'update-it').Href;

        const cell5 = document.createElement('td');
        cell5.innerHTML = `
        <button class="btn btn-danger" onclick="handleDeleteClick('${deleteUrl}')">Delete</button>
        <button class="btn btn-warning" onclick="handleUpdateClick(${student.Id}, '${updateUrl}')">Update</button>`;

        row.appendChild(cell1);
        row.appendChild(cell2);
        row.appendChild(cell3);
        row.appendChild(cell4);
        row.appendChild(cell5);

        studentsTableBody.appendChild(row);
    });

    studentsTable.innerHTML = null;
    studentsTable.appendChild(studentsTableBody);
}

function setListLinks(links, currentLink) {
    listLinks.FirstPageLink = links.find(x => x.Rel === 'first-page').Href;
    listLinks.LastPageLink = links.find(x => x.Rel === 'last-page').Href;
    listLinks.NextPageLink = links.find(x => x.Rel === 'next-page').Href;
    listLinks.PreviousPageLink = links.find(x => x.Rel === 'previous-page').Href;
    listLinks.CurrentPageLink = currentLink;
}

(async () => {
    const studentsResource = await loadStudentsResource(`http://localhost:44367/api/students/?limit=${limit}`);

    buildTable(studentsResource.Embedded);
    setListLinks(studentsResource.Links, `http://localhost:44367/api/students/?limit=${limit}`);
})();