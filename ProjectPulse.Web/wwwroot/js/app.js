function AdjustSidebar() {
    console.log("adjust sidebar invoked")
    const sidebar = document.getElementById('sidebar');
    const showSidebarBtn = document.getElementById('show-sidebar');
    const mainContent = document.getElementById('main-content');
    
    if (window.innerWidth < 768) {
        sidebar.classList.add('sidebar-hidden');
        mainContent.classList.add('main-expanded');
        showSidebarBtn.classList.remove('sidebar-show-hidden');
    }
}

function HandleCloseSidebar()
{
    console.log("Handle sidebar close invoked");
    const mainContent = document.getElementById('main-content');
    if (window.innerWidth < 768) {
        mainContent.classList.remove('main-hidden');
    }
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.add('sidebar-hidden');
    mainContent.classList.add('main-expanded');
    const showSidebarBtn = document.getElementById('show-sidebar');
    showSidebarBtn.classList.remove('sidebar-show-hidden');
}

function HandleOpenSidebar()
{
    console.log("Handle sidebar open invoked");
    const mainContent = document.getElementById('main-content');
    if (window.innerWidth < 768) {
        mainContent.classList.add('main-hidden');
    }
    const sidebar = document.getElementById('sidebar');
    sidebar.classList.remove('sidebar-hidden');
    mainContent.classList.remove('main-expanded');
    const showSidebarBtn = document.getElementById('show-sidebar');
    showSidebarBtn.classList.add('sidebar-show-hidden');
}