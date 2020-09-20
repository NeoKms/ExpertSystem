def print_m(m):
    for i in range(len(m)):
        print(m[i])
        
def delp(m):
    now_no_powt=[]
    for i in range(len(m)):
        now_no_powt.append("".join(set(m[i])))
    m=now_no_powt
    delo=[]
    for i in range(len(m)):
        for j in range(len(m)):
            if i==j:
                continue
            bol=True
            for dl in range(len(delo)):
                if i==delo[dl] or j==delo[dl]:
                    bol=False
            if bol:
                bol=True
                for ind in range(len(m[i])):
                    if m[j].count(m[i][ind])==0:
                        bol=False 
                if bol:
                    delo.append(j)
    d=[]
    for i in range(len(m)):
        bol=True
        for j in range(len(delo)):
            if i==delo[j]:
                bol=False
        if bol:
            d.append(m[i])
    return d

start=[ [1,0,1,1,1,0,1],
        [0,0,1,0,0,1,0],
        [1,1,0,1,0,0,1],
        [0,1,1,1,1,0,1],
        [1,1,1,0,1,1,1],
        [1,1,1,1,1,1,0]]
classes=[[0,1,2],[3,4],[5]]
testing=[1,1,0,0,1,0,0]
M=[]
Edi=[]
Ms=[]
#Нахождения матрицы М и Мс
for i in range(len(classes[0])):
    now=start[classes[0][i]]
    prov_class=1
    for j in range(len(classes[prov_class])):
        prov_string=start[classes[prov_class][j]]
        s_i=[]
        col=0
        for it in range(len(now)):
            if now[it]==prov_string[it]:
                s_i.append(0)
            else:
                s_i.append(1)
                col=col+1
        Edi.append(col)
        M.append(s_i)
        Ms.append(s_i)
    prov_class=prov_class+1
#print('m')
#print_m(M)
#print('ms')
#print_m(Ms)
#Нахождение индексов строк, которые надо удалить
ind_for_del=[]
for i in range(len(Edi)-1):
    if ind_for_del!=[]:
        if i==ind_for_del[-1]:
            continue
    now=M[i]
    if M[i]==[]:
        ind_for_del.append(i)
        continue
    for xi in range(i+1,len(Edi)):
        nex=M[xi]
        col=0
        for j in range(len(M[i])):
            if now[j]==nex[j] and now[j]==1:
                col=col+1
                if col==Edi[i]:
                    ind_for_del.append(xi)
                    break
#Удаление из матрицы Мс строк и получение матрицы Мсс
Mss=[]
for i in range(len(Ms)):
    bol=True
    for j in range(len(ind_for_del)):  
        if i==ind_for_del[j]:
            bol=False
            break
        bol=True
    if bol:
        Mss.append(Ms[i])
#Нахождение EП:
EP=[]
for i in range(len(Mss)):
    s_i=[]
    for j in range(len(Mss[i])):
        if Mss[i][j]==1:
            s_i.append(str(j+1))
    EP.append(s_i)
#print('mss')
#print_m(Mss)
#print('EP')
#print_m(EP)
    
#определение пустых из ЕП
ind_for_del=[]
Epm=[]
col=0
bol=True
for i in range(len(EP)):
    if EP[i]==[]:
        bol=False
        col=col+1
if bol==False:
    print("Изза совпадения объектов",col+1," классов задачу решить невозможно")
if bol:
#Раскрытие скобок ЕП: и получение термов
    # В C# именно здесь не работает
    now=EP[0]
    for i in range(1,len(EP)):
        nex=EP[i]
        si=[]
        for j in range(len(now)):
            for xi in range(len(nex)):
                si.append(now[j]+nex[xi])
        now=delp(si)
    now_2=now
#   
    count_P=[]
    for i in range(len(M[0])):
        col=0
        for j in range(len(now_2)):
            col=col+now_2[j].count(str(i+1))
        count_P.append(str(col)+'/'+str(len(now_2)))
#вывод инф.весов
    f = open('text.txt', 'w')
    print('Информационыне веса:')
    f.write('Информационыне веса:' + '\n')
    for i in range(len(count_P)):
        ss='P'+str(i)+'='+str(count_P[i])
        print(ss)
        f.write(ss + '\n')
        
#Прогон тупиковых тестов в тестовой выборке
    in_class=[]
    for i in range(len(classes)):#проход по всем классам
        #print('class=',i)
        in_test=[0 for c in now_2]
        for j in range(len(classes[i])):#проход внутри одного класса
            tec=classes[i][j]
            #print(start[tec])
            for i_now_2 in range(len(now_2)):#проход по всем тупиковым тестам
                col_in=0
                bol=True
                for in_str in range(len(now_2[i_now_2])):#проход по индексам тупиковых тестов
                    ind=int(now_2[i_now_2][in_str])-1
                    if testing[ind]!=start[tec][ind]:
                        bol=False
                if(bol):
                    col_in=col_in+1
                    in_test[i_now_2]=in_test[i_now_2]+1
        in_class.append(in_test)
#форматированный вывод вхождения термов
    str_1='Термы'
    str_2='Итого'
    for i in range(len(in_class)):
        str_1=str_1+'\t   Class'+str(i+1)
        
    print(str_1)
    f.write(str_1 + '\n')
    c = ['0' for c in now_2]
    for i in range(len(now_2)):
        c[i]=now_2[i]
    for i in range(len(in_class)):
        for j in range(len(in_class[i])):
            c[j]=c[j]+'\t\t'+str(in_class[i][j])
    print_m(c)
    for i in range(len(c)):
        f.write(c[i] + '\n')
        #all_str.append(print(in_class[i][j])
        
#Определение принадлежности к классу
    col_in_cl=[]
    for i in range(len(in_class)):
        col=0
        for j in range(len(in_class[i])):
            col=col+in_class[i][j]
        col_in_cl.append(col)
        str_2=str_2 + '\t\t' + str(col)
    print(str_2)
    f.write(str_2 + '\n')
    z = col_in_cl.index(max(col_in_cl))+1
    print('Тестовая выборка принадлежит классу ',z)
    f.write('Тестовая выборка принадлежит классу ' + str(z) + '\n')
    f.close()



















    
