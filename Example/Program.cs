// See https://aka.ms/new-console-template for more information
using System.Text.Json.Serialization;
using Example;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

var personalUsers = JsonConvert.DeserializeObject<IList<Personal>>(Datas.PersonalJson);
var studentUsers = JsonConvert.DeserializeObject<IList<Student>>(Datas.StudentJson) ;
var jobberUsers = JsonConvert.DeserializeObject<IList<Jobber>>(Datas.JobberJson);
IDictionary<string,IList<string>> indexes =new Dictionary<string,IList<string>>();
IDictionary<string, IUser> fastList = new Dictionary<string, IUser>();

fastList.AddToDictionary(studentUsers.Select(user=>(user as IUser)).ToList(),indexes);

//var allUsers = FindByIndex("Benjamen");
var allUsers = FindByIndex("Arnal");

allUsers.ToList().ForEach(user => Console.WriteLine("\n" + JsonConvert.SerializeObject(user)));
Console.ReadKey();

IList<IUser>? FindByIndex(string search)
{
    if (indexes.ContainsKey(search))
    {
        var findedKeys=indexes[search];
        return findedKeys.Select(key => fastList[key]).ToList();
    }
    return null;
}
public static class MicrosoftExtensions
{
   
    public static List<T> FindAll<T>(this IList<T> values, Predicate<T> predicate)
    {
        return values.ToList().FindAll(predicate);
    }
 
   
    public static void AddToDictionary<TKey,TValue>(
        this IDictionary<TKey, TValue> values, 
        IList<TValue> users,
        IDictionary<TKey,IList<TKey>> indexes
        )
        where TValue: IUser
        where TKey: notnull
    {
        TKey castToKey(object key)
        {
            return (TKey)key;
        };
        void addIndex(object findKeyObject, TKey dataKey)
        {
            TKey findKey= castToKey(findKeyObject);
            if (indexes.ContainsKey(findKey))
            {
                indexes[findKey].Add(dataKey);
            }
            else
            {
                indexes.Add(findKey, new List<TKey>() { dataKey });
            }
        };
        users?.ToList().ForEach(user =>
        {
            TKey key = castToKey(user.UserName);
            values.Add(key, user);
            addIndex(user.FirstName, key);
            addIndex(user.LastName, key);
            addIndex(user.Id, key);
           
            var personal = user.CastTo<IPersonal>();
            if (personal!=null){
                addIndex(personal.SSN, key);
                addIndex(personal.Salary.ToString(),key);
            }
            var student = user.CastTo<IStudent>();
            if (student != null)
            {
                addIndex(student.Absenteeism.ToString(), key);
                addIndex(student.StudentNo, key);
                addIndex(student.Marks.ToString(), key);
            }
            var jobber = user.CastTo<IJobber>();
            if (jobber != null)
            {
                addIndex(jobber.PlateNo, key);
                addIndex(jobber.WorkArea, key);
            }
        });
    }
    public static TObject? CastTo<TObject>(this object value)
        where TObject: class
    {
        if (value is TObject)
        {
            return value as TObject;
        }
        return null;
    }
}
