using System;
using System.Collections.Generic;

class NUser{
  private NUser() { }
  static NUser obj = new NUser();
  public static NUser Singleton{get => obj;}

  
  private List<User> users = new List<User>();

  public void Abrir(){
    Arquivo<List<User>> f = new Arquivo<List<User>>();
    users = f.Abrir("./users.xml");
  }
  public void Salvar(){
    Arquivo<List<User>> f = new Arquivo<List<User>>();
    f.Salvar("./users.xml",Listar());
  }
  
  public List<User> Listar(){
    // Retorna uma lista com os usuarios cadastrados 
    users.Sort();
    return users;
  }

  public User Listar(int id){
    // Localiza na list o usuario com o id informado
    for(int i = 0;i < users.Count;i++){
      if(users[i].Id == id) return users[i];
    }
    return null;
  }

  public void Inserir(User u){
    // Gera o id do usuario
    int max = 0;
    foreach(User obj in users){
      if(obj.Id > max) max = obj.Id;
    }
    u.Id =  max + 1;
    // Inserir usuario na lista
    users.Add(u);
  }

  public void Atualizar(User u){
    // Localizar na lista o usuario com o id informado
    User user_atual = Listar(u.Id);
    // Se não encontrar o usuario com o id informado, retorna sem alterar
    if (user_atual == null) return;
    // Alterar os dados do usuário
    user_atual.Nome = u.Nome;
  }

  public void Excluir(User u){
    // Remover o usuário da lista
    if(u != null) users.Remove(u);
  }
}