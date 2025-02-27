# WPFClientExample

## 📌 프로젝트 개요

**WPFClientExample**은 **Game 운영 Tool**을 모티브로 개발된 WPF 기반 클라이언트 앱만적입니다.  
MVVM 패턴을 적용하였고, DI(의졸성 주입)과 다양한 기능을 포함하여 실제 운영 도구처럼 동작하도록 구성하였습니다.

---

## 🛠️ 기술 스택

| 기술                       | 설명                                            |
| -------------------------- | ----------------------------------------------- |
| **.NET 9.0**               | 최신 .NET 기술 기반                             |
| **WPF**                    | Windows Presentation Foundation                 |
| **MVVM**                   | 구조적 패턴 적용                                |
| **DI (의졸성 주입)**       | `Microsoft.Extensions.DependencyInjection` 사용 |
| **CommunityToolkit.Mvvm**  | MVVM 패턴 지원                                  |
| **WeakReferenceMessenger** | 메시지 기반 이벤트 처리                         |
| **MSTest, Moq**            | 단위 테스트 적용                                |
| **XAML Styles**            | Custom Control 스태일 정의                      |

---

## ✨ 주요 특징

| 기능                          | 설명                                                                                                                  |
| ----------------------------- | --------------------------------------------------------------------------------------------------------------------- |
| **MVVM 패턴 적용**            | View와 ViewModel을 분리하여 유지보수성과 확장성 향상                                                                  |
| **Service & Repository 사용** | `Service`와 `Repository`를 활용하여 데이터 로직 분리 <br> (실제 서버 연동 시 `Repository` 없이 `Service`만 사용 가능) |
| **테스트 데이터 활용**        | `TestDataFactory`를 통해 하드코딩된 데이터를 제공 및 바인딩                                                           |
| **화면 전환 및 초기화**       | `INavigationService`를 활용하여 네비게이션 관리 및 View 초기화                                                        |
| **비동기 처리 테스트**        | `BillHistoryViewModel`, `ChatLogViewModel`에 `async/await` 및 `Thread.Sleep()` 적용                                   |
| **실시간 다국어 지원**        | **영어(en-US)** 및 **한국어(ko-KR)** 실시간 변환 가능                                                                 |
| **실시간 테마 및 편화**       | **기본(Default)** 및 **다크(Dark)** 테마 지원                                                                         |
| **폰트 및 UI 조정**           | 설정을 통해 클라이언트 내 폰트 및 UI 개조 가능                                                                        |

---

## 🛠️ 실행 방법

1. **프로젝트 빌드 및 실행**

   - `WPFClientExample.sln`을 Visual Studio에서 실행
   - `Start` 버튼을 클릭하여 앱을 실행

2. **로그인**

   - ID: `admin`
   - PW: `1234`

3. **기능 테스트**
   - User Info, Chat Log, Bill History 등 테스트 가능

---
