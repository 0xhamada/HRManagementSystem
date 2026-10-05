
async function loadMyProfile(url) 
{
    const modalBody = document.getElementById('profileModalBody');
    modalBody.innerHTML = '<p>Loading...</p>';

    try
    { 
        const response = await fetch(url);
        const result = await response.json();

        if (result.success)
        {
            const p = result.data;
            modalBody.innerHTML = `
            <h4>${p.name}</h4>
            <p class="text-muted">${p.jobTitle}</p>
            <hr>
            <p><strong>Username:</strong> ${p.userName}</p>
            <p><strong>Email:</strong> ${p.email}</p>
            <p><strong>Email:</strong> ${p.salary}</p>
            <p><strong>Age:</strong> ${p.age}</p>
            <p><strong>Department:</strong> ${p.departmentName ?? 'Not assigned'}</p>
            <p><strong>Hire Date:</strong> ${new Date(p.hireDate).toLocaleDateString()}</p>
            `;
        }
        else
        {
            modalBody.innerHTML = `<p class="text-danger">${result.message}</p>`;
        }
    }
    catch (error)
    {
        modalBody.innerHTML = '<p class="text-danger">Something went wrong loading your profile.</p>';
    }

                const modal = new bootstrap.Modal(document.getElementById('profileModal'));
                modal.show();
}
document.addEventListener("DOMContentLoaded", function () {
    const profileLink = document.getElementById("profileLink");
    if (profileLink)
    {
        profileLink.addEventListener("click", function (e) {
            e.preventDefault();
            loadMyProfile(this.dataset.url);
        });
    }
});