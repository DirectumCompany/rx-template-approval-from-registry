# Согласование от реестра
Репозиторий с шаблоном разработки "Согласование от реестра". Подробное описание см. в [Directum RX. Шаблон разработки Согласование от реестра_.Описание](https://github.com/DirectumCompany/rx-template-approval-from-registry/tree/main/docs).
> [!NOTE]
> Замечания и пожеланию по развитию шаблона разработки фиксируйте через [Issues](https://github.com/DirectumCompany/rx-template-approval-from-registry/issues).
При оформлении ошибки, опишите сценарий для воспроизведения. Для пожеланий приведите обоснование для описываемых изменений - частоту использования, бизнес-ценность, риски и/или эффект от реализации.
> 
> Внимание! Изменения будут вноситься только в новые версии.

## Описание
Решение позволяет сотрудникам:
-	согласовывать документы из карточки документа, без использования задачи на согласование по процессу;
-	согласовывать несколько документов из списка.

Работа с согласованием документов осуществляется из карточки документа, или из списка, с помощью действий на ленте. Шаблон разработки содержит перекрытие базового документа (OfficialDocument). В карточку добавлены дополнительные свойства и действия, для того чтобы 
можно было согласовать документ.

## Порядок установки
Для работы требуется установленный Directum RX версии 4.10 и выше.

## Установка для ознакомления
1. Склонировать репозиторий с rx-template-approval-from-registry в папку.
2. Указать в _ConfigSettings.xml DDS:
<block name="REPOSITORIES">
  <repository folderName="Base" solutionType="Base" url="" /> 
  <repository folderName="<Папка из п.1>" solutionType="Work" 
     url="https://github.com/DirectumCompany/rx-template-approval-from-registry" />
</block>

## Установка для использования на проекте
Возможные варианты

**A. Fork репозитория**
1. Сделать fork репозитория rx-template-approval-from-registry для своей учетной записи.
2. Склонировать созданный в п. 1 репозиторий в папку.
3. Указать в _ConfigSettings.xml DDS:
<block name="REPOSITORIES">
  <repository folderName="Base" solutionType="Base" url="" /> 
  <repository folderName="<Папка из п.2>" solutionType="Work" 
     url="https://github.com/DirectumCompany/rx-template-approval-from-registry" />
</block>

**B. Подключение на базовый слой.**
Вариант не рекомендуется, так как при выходе версии шаблона разработки не гарантируется обратная совместимость.
1. Склонировать репозиторий rx-template-approval-from-registry в папку.
2. Указать в _ConfigSettings.xml DDS:
<block name="REPOSITORIES">
  <repository folderName="Base" solutionType="Base" url="" /> 
  <repository folderName="<Папка из п.1>" solutionType="Base" 
     url="<Адрес репозитория gitHub>" />
  <repository folderName="<Папка для рабочего слоя>" solutionType="Work" 
     url="<Адрес репозитория для рабочего слоя>" />
</block>

**C. Копирование репозитория в систему контроля версий.**
Рекомендуемый вариант для проектов внедрения.
1. В системе контроля версий с поддержкой git создать новый репозиторий.
2. Склонировать репозиторий <Название репозитория> в папку с ключом --mirror.
3. Перейти в папку из п. 2.
4. Импортировать клонированный репозиторий в систему контроля версий командой:
git push –mirror <Адрес репозитория из п. 1>
