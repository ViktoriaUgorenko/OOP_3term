using System;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace laba13
{
    [Serializable]
    public class UIElement
    {
        public string Id { get; set; }
    }

    [Serializable]
    public class Button : UIElement
    {
        public string Color { get; set; }
        public string ButtonText { get; set; }

        [NonSerialized]
        public string InternalData; 

        public Button() { }

        public Button(string id, string color, string buttonText, string internalData)
        {
            Id = id;
            Color = color;
            ButtonText = buttonText;
            InternalData = internalData;
        }

        public override string ToString()
        {
            return $"Button{{ Id='{Id}', Color='{Color}', ButtonText='{ButtonText}', InternalData='{InternalData}' }}";
        }
    }

    public interface ISerializer
    {
        void Serialize<T>(T obj, string filePath);
        T Deserialize<T>(string filePath);
    }

    public class BinarySerializer : ISerializer
    {
        public void Serialize<T>(T obj, string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, obj);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            var formatter = new BinaryFormatter();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public class SoapSerializer : ISerializer
    {
        public void Serialize<T>(T obj, string filePath)
        {
            var formatter = new SoapFormatter();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                formatter.Serialize(stream, obj);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            var formatter = new SoapFormatter();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return (T)formatter.Deserialize(stream);
            }
        }
    }

    public class JsonSerializer : ISerializer
    {
        public void Serialize<T>(T obj, string filePath)
        {
            var json = System.Text.Json.JsonSerializer.Serialize(obj, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public T Deserialize<T>(string filePath)
        {
            var json = File.ReadAllText(filePath);
            return System.Text.Json.JsonSerializer.Deserialize<T>(json);
        }
    }

    public class XmlSerializerWrapper : ISerializer
    {
        public void Serialize<T>(T obj, string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                serializer.Serialize(stream, obj);
            }
        }

        public T Deserialize<T>(string filePath)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(stream);
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Button[] buttons = new Button[]
            {
                new Button("1", "красный", "нажми меня", "скрытая информация 1"),
                new Button("2", "зеленый", "жми", "скрытая информация 2"),
                new Button("3", "синий", "кликни", "скрытая информация 3")
            };

            string binaryFilePath = "buttons_binary.dat";
            string soapFilePath = "buttons_soap.dat";
            string jsonFilePath = "buttons.json";
            string xmlFilePath = "buttons.xml";

            var binarySerializer = new BinarySerializer();
            binarySerializer.Serialize(buttons, binaryFilePath);
            var binaryDeserialized = binarySerializer.Deserialize<Button[]>(binaryFilePath);
            Console.WriteLine("десериализация Binary:");
            foreach (var button in binaryDeserialized)
            {
                Console.WriteLine(button);
            }

            var soapSerializer = new SoapSerializer();
            soapSerializer.Serialize(buttons, soapFilePath);
            var soapDeserialized = soapSerializer.Deserialize<Button[]>(soapFilePath);
            Console.WriteLine("\nдесериализация SOAP:");
            foreach (var button in soapDeserialized)
            {
                Console.WriteLine(button);
            }

            var jsonSerializer = new JsonSerializer();
            jsonSerializer.Serialize(buttons, jsonFilePath);
            var jsonDeserialized = jsonSerializer.Deserialize<Button[]>(jsonFilePath);
            Console.WriteLine("\nдесериализация JSON:");
            foreach (var button in jsonDeserialized)
            {
                Console.WriteLine(button);
            }

            var xmlSerializer = new XmlSerializerWrapper();
            xmlSerializer.Serialize(buttons, xmlFilePath);
            var xmlDeserialized = xmlSerializer.Deserialize<Button[]>(xmlFilePath);
            Console.WriteLine("\nдесериализация XML:");
            foreach (var button in xmlDeserialized)
            {
                Console.WriteLine(button);
            }

            Console.WriteLine("\nXPath запросы:");
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlFilePath);

            var buttonNodes = xmlDoc.SelectNodes("//Button");
            foreach (XmlNode node in buttonNodes)
            {
                Console.WriteLine($"XPath 1 - Button: {node.OuterXml}");
            }

            var redButtonTextNodes = xmlDoc.SelectNodes("//Button[Color='красный']/ButtonText");
            foreach (XmlNode node in redButtonTextNodes)
            {
                Console.WriteLine($"XPath 2 - текст кнопки: {node.InnerText}");
            }

            Console.WriteLine("\nLinq to XML:");
            var newXml = new XElement("Buttons",
                from button in buttons
                select new XElement("Button",
                    new XElement("Id", button.Id),
                    new XElement("Color", button.Color),
                    new XElement("ButtonText", button.ButtonText)
                )
            );

            string newXmlPath = "new_buttons.xml";
            newXml.Save(newXmlPath);
            Console.WriteLine($"новый XML сохранен в {newXmlPath}");
        }
    }
}
