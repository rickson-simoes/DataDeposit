﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataDeposit.Models
{
  public class CareerItem
  {
    public Guid Id { get; set; }
    public string Title { get; set; }
    public Course Course { get; set; }
  }
}
