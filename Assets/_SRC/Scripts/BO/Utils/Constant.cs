using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constant
{
    public const int CLIENT_STATUS_NO_SURVEY = 1;
    public const int CLIENT_STATUS_NO_ROUTINE = 2;
    public const int CLIENT_STATUS_WAITING = 3;
    public const int CLIENT_STATUS_HAS_ROUTINE = 4;
    public const int CLIENT_STATUS_BANNED = 5;

    public const int ACTIVITY_TYPE_REPS = 1;
    public const int ACTIVITY_TYPE_TIMER = 2;
    public static readonly DateTime DefaultDateTime = DateTime.ParseExact("1998-10-11", "yyyy-mm-dd", null);

    public const string DEFAULT_API_DATE_FORMAT = "yyyy-MM-dd";
}
