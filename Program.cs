using System;
using System.IO;
using System.Net.Mail;
using System.Data;
using System.Collections.Generic;

namespace Job
{
    public class Project
    {
        class Program
        {
                
            public static void email_send()
            {
                Console.WriteLine("Sender e-mail: ");
                string sender = Console.ReadLine();
                Console.WriteLine("Sender e-mail password: ");
                string password = Console.ReadLine();
                Console.WriteLine("Receiver e-mail: ");
                string receiver = Console.ReadLine();
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.outlook.com");
                mail.From = new MailAddress(sender);
                mail.To.Add(receiver);
                mail.Subject = "Test Mail - 1";
                mail.Body = "mail with attachment";

                System.Net.Mail.Attachment attachment;
                attachment = new System.Net.Mail.Attachment(@"C:\Users\VIP\Desktop\ReportByCountry.csv");
                mail.Attachments.Add(attachment);

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(sender, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
            }

            static void Main(string[] args)
            {
                Console.WriteLine("Enter the input file path: ");
                string file = Console.ReadLine();
                string area = null;
                int average1 = 0, average2 = 0, average3 = 0;
                int medial1 = 0, medial2 = 0, medial3 = 0;
                int max1 = 0, max2 = 0, max3 = 0;
                int min1 = 1000, min2 = 1000, min3 = 1000;
                int count1 = 0, count2 = 0, count3 = 0;
                string maxp1 = null, maxp2 = null, maxp3 = null;
                string minp1 = null, minp2 = null, minp3 = null;                
                var csvTable = new DataTable();
                using (var csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(new StreamReader(System.IO.File.OpenRead(@file)), true))
                {

                    csvTable.Load(csvReader);
                    for (int i = 0; i < 4; i++)
                    {
                        area = csvTable.Rows[i][2].ToString();
                        string r1 = csvTable.Rows[i][4].ToString();
                        string r2 = csvTable.Rows[i][4].ToString();
                        string r3 = csvTable.Rows[i][4].ToString();
                        string r4 = csvTable.Rows[i][4].ToString();
                        int r1_score = Int32.Parse(r1);
                        int r2_score = Int32.Parse(r2);
                        int r3_score = Int32.Parse(r3);
                        int r4_score = Int32.Parse(r4);
                        int[] sc = { r1_score, r2_score, r3_score, r4_score };

                        if (area == "Bulgaria")
                        {
                            count1++;
                            int sum = +sc[i];
                            average1 = sum / count1;
                            medial1 = sc[i / 2];
                            if (sc[i] > max1)
                            {
                                max1 = sc[i];
                                maxp1 = csvTable.Rows[i][0].ToString() + " " + csvTable.Rows[i][1].ToString();
                            }
                            if (min1 > sc[i])
                            {
                                min1 = sc[i];
                                minp1 = csvTable.Rows[i][0].ToString() + " " + csvTable.Rows[i][1].ToString();
                            }
                        }
                        if (area == "Germany")
                        {
                            count2++;
                            int sum = +sc[i];
                            average2 = sum / count2;
                            medial2 = sc[i / 2];
                            if (sc[i] > max2)
                            {
                                max2 = sc[i];
                                maxp2 = csvTable.Rows[i][0].ToString() + " " + csvTable.Rows[i][1].ToString();
                            }
                            if (min2 > sc[i])
                            {
                                min2 = sc[i];
                                minp2 = csvTable.Rows[i][0].ToString() + " " + csvTable.Rows[i][1].ToString();
                            }

                        }
                        if (area == "England" || area == "Great Britain")
                        {
                            count3++;
                            int sum = +sc[i];
                            average3 = sum / count3;
                            medial3 = sc[i / 2];
                            if (sc[i] > max3)
                            {
                                max3 = sc[i];
                                maxp3 = csvTable.Rows[i][0].ToString() + " " + csvTable.Rows[i][1].ToString();
                            }
                            if (min3 > sc[i])
                            {
                                min3 = sc[i];
                                minp3 = csvTable.Rows[i][0].ToString() + " " + csvTable.Rows[i][1].ToString();
                            }
                        }
                    }
                }
                var textReader = new StreamReader(@file);
                var csv = new CsvHelper.CsvReader(textReader, System.Globalization.CultureInfo.CurrentCulture);
                var records = csv.GetRecords<Program>();

                string path = @"C:\Users\VIP\Desktop\ReportByCountry.csv";
                string delimiter = ", ";

                if (!File.Exists(path))
                {
                    string createText = "Country" + delimiter + "Average score" + delimiter + "Median score" + delimiter + "Max score" + delimiter + "Max score person" + delimiter + "Min score" + delimiter + "Min score person" + delimiter + "Record count" + delimiter + Environment.NewLine;
                    File.WriteAllText(path, createText);
                }
                string appendText = "Bulgaria" + delimiter + average1 + delimiter + medial1 + delimiter + max1 + delimiter + maxp1 + delimiter + min1 + delimiter + minp1 + delimiter + count1 + Environment.NewLine +
                        "Germany" + delimiter + average2 + delimiter + medial2 + delimiter + max2 + delimiter + maxp2 + delimiter + min2 + delimiter + minp2 + delimiter + count2 + Environment.NewLine +
                        "England" + delimiter + average3 + delimiter + medial3 + delimiter + max3 + delimiter + maxp3 + delimiter + min3 + delimiter + minp3 + delimiter + count3 + Environment.NewLine;
                File.AppendAllText(path, appendText);

                string readText = File.ReadAllText(path);
                Console.WriteLine(readText);
                email_send();
            }
        }
    }
}


