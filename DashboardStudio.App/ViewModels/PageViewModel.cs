using System;
using System.Collections.Generic;
using System.Text;
using DashboardStudio.Models.Dashboards;

namespace DashboardStudio.App.ViewModels;

public sealed class PageViewModel
{
    private readonly Page page;

    public Guid Id => page.Id;

    public string Name => page.Name;

    public int Order => page.Order;

    public PageViewModel(Page page)
    {
        this.page = page;
    }
}