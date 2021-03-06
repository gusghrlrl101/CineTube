﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace Cinetube.Models
{
    public class GlobalVariables
    {
        delegate List<string> GetHintList();

        private static readonly GetHintList getHintList = () =>
        {
            List<string> list = new List<string>();
            using (var connection =
                new SqlConnection(connectionUrl))
            {
                var command = new SqlCommand(
                    "SELECT 힌트번호, 힌트질문  FROM 비밀번호힌트",
                    connection);
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var hintNo = Convert.ToInt32(reader[0]);
                        var hintStr = Convert.ToString(reader[1]);
                        //Console.WriteLine($"힌트번호: {hintNo}, 힌트: {hintStr}");
                        list.Add(hintStr);
                    }
                }
            }

            return list;
        };

        public static readonly string connectionUrl =
            "server = sappho192.iptime.org,21433;database = CinetubeDB2;uid=cinetube;pwd=qwer12#$;";

        public static List<string> PwHintList = getHintList();

        public static readonly Regex PhoneRegex = new Regex(@"\d{11}");

        public static readonly List<string> StringFilter = new List<string> { "제목", "줄거리", "관람제한", "제작사", "감독", "장르", "배우" };
        public static readonly List<string> IntFilter = new List<string> { "금액", "개봉연도" };
    }
}
