$(() => {
    loadPeople();

    function loadPeople() {
        $.get('/home/getall', function (people) {
            $("#people-table tr:gt(0)").remove();
            people.forEach(person => {
                $('#people-table tbody').append(`
<tr>
    <td>${person.firstName}</td>
    <td>${person.lastName}</td>
    <td>${person.age}</td>
<td>
<button class= "btn btn-danger btn-block delete-person"  data-id=${person.id}>Delete</button>
</td>

<td>
<button class= "btn btn-info btn-block edit-person" data-id='${person.id}' data-first='${person.firstName}' data-last=${person.lastName} data-age =${person.age}>Edit</button>
</td>
</tr>`);
            });
        });
    }

    $("#add-person").on('click', function () {       
        const firstName = $('#first-name').val();
        const lastName = $("#last-name").val();
        const age = $("#age").val();

        console.log(`${firstName} ${lastName} - ${age}`)

        $.post('/home/addperson'), { firstName, lastName, age }, function () {
            loadPeople();
           
            $("#first-name").val('');
            $("#last-name").val('');
            $("#age").val('');
        };      
    });
});