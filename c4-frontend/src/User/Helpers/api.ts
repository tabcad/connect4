export async function postSignup(details: AccountDetails) {
  const url = "https://localhost:7018/api/user/signup";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: details.username,
      password: details.password,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error creating account";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

// Error: response status is 500

// Response body
// Download
// {
//   "error": "An unexpected error has occurred",
//   "details": "Failed to create user: Passwords must have at least one non alphanumeric character., Passwords must have at least one digit ('0'-'9')., Passwords must have at least one uppercase ('A'-'Z').",
//   "type": "InvalidOperationException"
// }

export async function postLogin(details: LoginDetails) {
  const url = "https://localhost:7018/api/user/login";
  const response = await fetch(url, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      username: details.username,
      password: details.password,
    }),
  });

  if (!response.ok) {
    const fallbackError = "Error logging in";

    let data;
    try {
      data = await response.json();
    } catch {
      throw new Error(fallbackError);
    }

    const errorMsg = data?.detail ?? fallbackError;
    throw new Error(errorMsg);
  }

  return await response.json();
}

//types

export interface LoginDetails {
  username: string;
  password: string;
}

export interface AccountDetails {
  username: string;
  password: string;
}
