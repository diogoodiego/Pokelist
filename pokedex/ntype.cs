using System;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Linq;

class NType{
  private NType() { }
  static NType obj = new NType();
  public static NType Singleton{get => obj;}
  
  private Type[] types = new Type[10];
  private int nt;

  public void Abrir(){
    Arquivo<Type[]> f = new Arquivo<Type[]>();
    types = f.Abrir("./types.xml");
    nt = types.Length;
    
    // XmlSerializer xml = new XmlSerializer(typeof(Type[])); 
    // StreamReader f = new StreamReader("./types.xml",Encoding.Default);
    // types = (Type[]) xml.Deserialize(f);
    // f.Close();
  }
  public void Salvar(){
    Arquivo<Type[]> f = new Arquivo<Type[]>();
    f.Salvar("./types.xml",Listar());
    
    // XmlSerializer xml = new XmlSerializer(typeof(Type[])); 
    // StreamWriter f = new StreamWriter("./types.xml",false,Encoding.Default);
    // xml.Serialize(f, Listar());
    // f.Close();
  }

  public Type[] Listar(){
    // Type[] t = new Type[nt];
    // Array.Copy(types, t, nt);
    // t.OrderBy(obj => obj.GetDescription());
    return types.Take(nt).OrderBy(obj => obj.GetId()).ToArray();
  }

  public Type Listar(int id){
    // for(int i = 0; i < nt; i++){
    //   if(types[i].GetId() == id) return types[i];
    // }
    // return null;
    // var r = types.Where(obj => obj.GetId() == id); 
    // return r.Count() == 0 ? null : r.First();
    return types.FirstOrDefault(obj => obj.GetId() == id);
  }

  public void Inserir(Type t){
    if(nt == types.Length){
      Array.Resize(ref types, 2 * types.Length);
    }
    types[nt] = t;
    nt++;
  }

  public void Atualizar(Type t){
    Type t_atual = Listar(t.GetId());
    if(t_atual == null){ 
      return;
    }
    t_atual.SetDescription(t.GetDescription());
  }

  private int Indice(Type t){
    for(int i = 0; i < nt; i++){
      if(types[i] == t) return i;
    }
    return -1;
  }

  public void Excluir(Type t){
    int n = Indice(t);
    if(n == -1) return;
    for(int i = n; i < nt - 1; i++){
      types[i] = types[i + 1];
    }
    nt--;

    Pokemon[] ps = t.PokemonListar();
    foreach(Pokemon p in ps){
      p.SetType(null);
    }
  }
  
}
