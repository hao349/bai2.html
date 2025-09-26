@model TourBookingApp.ViewModels.TourListViewModel
@{
    ViewData["Title"] = "Tours";
}

<h1>Tours</h1>

<form asp-action="Index" method="post" class="form-inline mb-3">
    <input type="text" name="searchTerm" value="@Model.SearchTerm" class="form-control" placeholder="Search tour name..." />
    <button type="submit" class="btn btn-primary ml-2">Search</button>
</form>

<form asp-action="Index" method="post" class="form-inline mb-3">
    <label class="mr-2">Show tours longer than</label>
    <input type="number" name="minDuration" value="@Model.MinDuration" min="1" class="form-control" />
    <span class="ml-2">days</span>
    <button type="submit" class="btn btn-secondary ml-2">Filter</button>
</form>

<p>
    Sort by:
    <a asp-action="Index" asp-route-sortOrder="name">Name ↑</a> |
    <a asp-action="Index" asp-route-sortOrder="name_desc">Name ↓</a> |
    <a asp-action="Index" asp-route-sortOrder="price">Price ↑</a> |
    <a asp-action="Index" asp-route-sortOrder="price_desc">Price ↓</a> |
    <a asp-action="Index" asp-route-sortOrder="duration">Duration ↑</a> |
    <a asp-action="Index" asp-route-sortOrder="duration_desc">Duration ↓</a>
</p>

<table class="table table-bordered table-striped">
    <thead>
        <tr>
            <th>Name</th>
            <th>Price</th>
            <th>Duration (days)</th>
            <th>Description</th>
        </tr>
    </thead>
    <tbody>
    @foreach (var tour in Model.Tours)
    {
        <tr>
            <td>@tour.Name</td>
            <td>@tour.Price.ToString("C")</td>
            <td>@tour.Duration</td>
            <td>@tour.Description</td>
        </tr>
    }
    </tbody>
</table>

<nav aria-label="Page navigation">
    <ul class="pagination">
        @for (int i = 1; i <= Model.TotalPages; i++)
        {
            <li class="page-item @(i == Model.CurrentPage ? "active" : "")">
                <a class="page-link"
                   asp-action="Index"
                   asp-route-page="@i"
                   asp-route-sortOrder="@Model.SortOrder"
                   asp-route-searchTerm="@Model.SearchTerm"
                   asp-route-minDuration="@Model.MinDuration">
                    @i
                </a>
            </li>
        }
    </ul>
</nav>
@model TourBookingApp.Models.Booking
@{
    ViewData["Title"] = "Create Booking";
}

<h1>Create Booking</h1>

<form asp-action="Create" method="post">
    <div class="form-group">
        <label asp-for="BookingDate"></label>
        <input asp-for="BookingDate" class="form-control" type="datetime-local" />
        <span asp-validation-for="BookingDate" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="CustomerName"></label>
        <input asp-for="CustomerName" class="form-control" />
        <span asp-validation-for="CustomerName" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="CustomerEmail"></label>
        <input asp-for="CustomerEmail" class="form-control" />
        <span asp-validation-for="CustomerEmail" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="TourId"></label>
        <select asp-for="TourId" asp-items="ViewBag.TourId" class="form-control"></select>
        <span asp-validation-for="TourId" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="NumberOfPeople"></label>
        <input asp-for="NumberOfPeople" class="form-control" />
        <span asp-validation-for="NumberOfPeople" class="text-danger"></span>
    </div>
    <button type="submit" class="btn btn-primary">Save Booking</button>
    <a asp-controller="Tours" asp-action="Index" class="btn btn-secondary">Back</a>
</form>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
@model TourBookingApp.Models.Booking
@{
    ViewData["Title"] = "Edit Booking";
}

<h1>Edit Booking</h1>

<form asp-action="Edit" method="post">
    <input type="hidden" asp-for="Id" />
    <div class="form-group">
        <label asp-for="BookingDate"></label>
        <input asp-for="BookingDate" class="form-control" type="datetime-local" />
        <span asp-validation-for="BookingDate" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="CustomerName"></label>
        <input asp-for="CustomerName" class="form-control" />
        <span asp-validation-for="CustomerName" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="CustomerEmail"></label>
        <input asp-for="CustomerEmail" class="form-control" />
        <span asp-validation-for="CustomerEmail" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="TourId"></label>
        <select asp-for="TourId" asp-items="ViewBag.TourId" class="form-control"></select>
        <span asp-validation-for="TourId" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="NumberOfPeople"></label>
        <input asp-for="NumberOfPeople" class="form-control" />
        <span asp-validation-for="NumberOfPeople" class="text-danger"></span>
    </div>
    <button type="submit" class="btn btn-primary">Save Changes</button>
    <a asp-controller="Tours" asp-action="Index" class="btn btn-secondary">Back</a>
</form>

@section Scripts {
    @{await Html.RenderPartialAsync("_ValidationScriptsPartial");}
}
@model TourBookingApp.Models.Booking
@{
    ViewData["Title"] = "Delete Booking";
}

<h1>Delete Booking</h1>

<h4>Are you sure you want to delete this booking?</h4>
<div>
    <dl class="row">
        <dt class="col-sm-2">Customer</dt>
        <dd class="col-sm-10">@Model.CustomerName</dd>
        <dt class="col-sm-2">Email</dt>
        <dd class="col-sm-10">@Model.CustomerEmail</dd>
        <dt class="col-sm-2">Tour</dt>
        <dd class="col-sm-10">@Model.Tour?.Name</dd>
        <dt class="col-sm-2">People</dt>
        <dd class="col-sm-10">@Model.NumberOfPeople</dd>
    </dl>
    <form asp-action="Delete" method="post">
        <input type="hidden" asp-for="Id" />
        <button type="submit" class="btn btn-danger">Delete</button>
        <a asp-controller="Tours" asp-action="Index" class="btn btn-secondary">Cancel</a>
    </form>
</div>
