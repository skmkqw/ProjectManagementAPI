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

function HandleProjectTabClick()
{
    console.log("Handle project tab click invoked");
    const projectTabs = document.querySelectorAll('.sidebar-projects-content-item');
    
    projectTabs.forEach(projectTab => {
        projectTab.addEventListener('click', ProjectClickEventListener);
    });
    
    function ProjectClickEventListener(event) {
        
        const sidebar = document.getElementById('sidebar');
        const optionTabs = document.querySelectorAll('.sidebar-options-content-item'); 
        const showSidebarBtn = document.getElementById('show-sidebar');
        const mainContent = document.getElementById('main-content');

        optionTabs.forEach(tab => tab.classList.remove('sidebar-options-content-item-active'));
        projectTabs.forEach(tab => tab.classList.remove('sidebar-projects-content-item-active'));
        
        if (window.innerWidth < 768) {
            sidebar.classList.add('sidebar-hidden');
            mainContent.classList.add('main-expanded');
            showSidebarBtn.classList.remove('sidebar-show-hidden');
            mainContent.classList.remove('main-hidden');
        }
        event.currentTarget.classList.add('sidebar-projects-content-item-active');
    }
}

function HandleOptionTabClick()
{
    console.log("Handle option tab click invoked");
    const optionTabs = document.querySelectorAll('.sidebar-options-content-item');
    
    const projectTabs = document.querySelectorAll('.sidebar-projects-content-item');

    optionTabs.forEach(optionTab => {
        optionTab.addEventListener('click', OptionClickEventListener);
    });

    function OptionClickEventListener(event) {

        const sidebar = document.getElementById('sidebar');
        const optionTabs = document.querySelectorAll('.sidebar-options-content-item');
        const showSidebarBtn = document.getElementById('show-sidebar');
        const mainContent = document.getElementById('main-content');

        optionTabs.forEach(tab => tab.classList.remove('sidebar-options-content-item-active'));
        projectTabs.forEach(tab => tab.classList.remove('sidebar-projects-content-item-active'));

        if (window.innerWidth < 768) {
            sidebar.classList.add('sidebar-hidden');
            mainContent.classList.add('main-expanded');
            showSidebarBtn.classList.remove('sidebar-show-hidden');
            mainContent.classList.remove('main-hidden');
        }
        event.currentTarget.classList.add('sidebar-options-content-item-active');
    }
}